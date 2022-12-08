using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Proyecto26;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    [SerializeField] private Stack<Card> cards;
    private HandleCSVFile reader = new HandleCSVFile();

    public Slider nCardsSlider;
    private float nCardsSliderValue;

    GameObject actualCardGO;
    CardLogic actualCardCL;

    public TextMeshProUGUI sentence;
    public TextMeshProUGUI nameCharachter;

    public AgentSlider teamAgent;
    public AgentSlider moneyAgent;
    public AgentSlider clientAgent;
    public AgentSlider natureAgent;

    public Sprite[] managersImg = new Sprite[9];

    //Timer
    public TextMeshProUGUI timerTmp;
    public int minSesionDuration;
    public DateTime finishTime;

    public Slider sliderTime;
    public GameObject timeOutCanvas;

    bool endTimer = false;

    //Tutorial:
    public GameObject tutorialPanel;

    void Start()
    {
        cards = new Stack<Card>();
        reader.ReadFile();
        generateStack();
        nCardsSliderValue = 0;

        finishTime = DateTime.Now.AddMinutes(minSesionDuration);       

    }

    // Update is called once per frame
    void Update()
    {
        if (endGameSession() && !endTimer)
        {
            Debug.Log("DONE");
            GameMemory.powers = getSlidersValue();
            timeOutCanvas.SetActive(true);
            Destroy(actualCardGO);
            cards.Clear();

            sendGame();
            endTimer = true;
        }
        else if (!endTimer) 
        {
            if (!endGameCondController())
            {
                if (actualCardGO == null && cards.Count > 0)
                {
                    actualCardGO = Instantiate(cardPrefab);
                    actualCardCL = actualCardGO.GetComponent<CardLogic>();

                    actualCardCL.SetCard(cards.Pop());
                    sentence.text = actualCardCL.GetSentence();
                    nameCharachter.text = actualCardCL.GetManager();
                }
                else
                {
                    //Check direction
                    if (actualCardGO.transform.position.x > 0.75f) //Right
                    {
                        actualCardCL.imgCard.color = new Color(0.4f, 0.4f, 0.4f);
                        actualCardCL.RightText();
                        if (!Input.GetMouseButton(0)&&tutorialPanel==null)
                        {
                            UpdateAgents();

                            GameMemory.cardCount++;
                            Destroy(actualCardGO);

                            nCardsSliderValue = (float)GameMemory.cardCount / (float)GameMemory.totalCards;
                            nCardsSlider.value = nCardsSliderValue;
                        }
                    }
                    else if (actualCardGO.transform.position.x < -0.75f) //Left
                    {
                        actualCardCL.imgCard.color = new Color(0.4f, 0.4f, 0.4f);
                        actualCardCL.LeftText();
                        if (!Input.GetMouseButton(0) && tutorialPanel == null)
                        {
                            UpdateAgents(true);

                            GameMemory.cardCount++;
                            Destroy(actualCardGO);

                            nCardsSliderValue = (float)GameMemory.cardCount / (float)GameMemory.totalCards;
                            nCardsSlider.value = nCardsSliderValue;
                        }
                    }
                    else
                    {
                        actualCardCL.imgCard.color = new Color(1f, 1f, 1f);
                        actualCardCL.NoneText();
                    }
                }
            }
            else
            {
                GameMemory.powers = getSlidersValue();
                sendGame();
                SceneManager.LoadScene("GameOver");
            }

        }

    }

    void UpdateAgents(bool _isLeftDesicion = false)
    {
        teamAgent.updateAgent(actualCardCL.GetAffection(_isLeftDesicion, teamAgent.agentType));
        moneyAgent.updateAgent(actualCardCL.GetAffection(_isLeftDesicion, moneyAgent.agentType));
        clientAgent.updateAgent(actualCardCL.GetAffection(_isLeftDesicion, clientAgent.agentType));
        natureAgent.updateAgent(actualCardCL.GetAffection(_isLeftDesicion, natureAgent.agentType));
    }

    float[] getSlidersValue()
    {
        float[] tmp = new float[4];
        tmp[0] = (float)teamAgent.sliderAgent.GetComponent<AgentSlider>().nextValue;
        tmp[1] = (float)moneyAgent.sliderAgent.GetComponent<AgentSlider>().nextValue;
        tmp[2] = (float)clientAgent.sliderAgent.GetComponent<AgentSlider>().nextValue;
        tmp[3] = (float)natureAgent.sliderAgent.GetComponent<AgentSlider>().nextValue;

        return tmp;
    }

    void generateStack()
    {
        for (int i = reader.cardsInfo.Count-1; i > -1; i--)
        {
            Affection[] affectionsLeft = new Affection[4];
            Affection[] affectionsRight = new Affection[4];

            for (int j = 0; j < 4; j++)
            {
                affectionsLeft[j] = getAffectionFromReader((string)reader.cardsInfo[i].GetValue(j + 1));
                affectionsRight[j] = getAffectionFromReader((string)reader.cardsInfo[i].GetValue(j + 5));
            }

            Manager tmpManager = getManagerFromReader((string)reader.cardsInfo[i].GetValue(0));

            cards.Push(new Card(affectionsLeft, affectionsRight,
            managersImg[(int)tmpManager], tmpManager,
            (string)reader.cardsInfo[i].GetValue(9),
            (string)reader.cardsInfo[i].GetValue(10), (string)reader.cardsInfo[i].GetValue(11)));
        }

        GameMemory.totalCards = cards.Count;
    }

    Affection getAffectionFromReader(string s)
    {
        switch (s)
        {
            case "0,2":
                return Affection.VERY_POSITIVE;
            case "0,1":
                return Affection.POSITIVE;
            case "-0,1":
                return Affection.NEGATIVE;
            case "-0,2":
                return Affection.VERY_NEGATIVE;
            default:
                return Affection.NEUTRAL;
        }

    }

    Manager getManagerFromReader(string s)
    {
        switch (s)
        {
            case "AYUDANTE":
                return Manager.AYUDANTE;
            case "CONTABLE":
                return Manager.CONTABLE;
            case "JEFE DE TIENDA":
                return Manager.JEFE_DE_TIENDA;
            case "ALDEANO":
                return Manager.ALDEANO;
            case "HOMBRE MISTERIOSO":
                return Manager.HOMBRE_MISTERIOSO;
            case "AVENTURERO":
                return Manager.AVENTURERO;
            case "PISTOLA":
                return Manager.PISTOLA;
            case "LIQUIDO":
                return Manager.LIQUIDO;
            case "DEFAULT":
                return Manager.DEFAULT;
            default:
                return Manager.DEFAULT;

        }
    }
    private bool endGameCondController()
    {

        //Mira si hay algun valor de los poderes a 0 para finalizar el juego.
        if (teamAgent.sliderAgent.value <= 0 || moneyAgent.sliderAgent.value <= 0 || clientAgent.sliderAgent.value <= 0 || natureAgent.sliderAgent.value <= 0)
        {

            return true;
        }
        
        //si no, mira si quedan cartas en la baraja
        if (cards.Count <= 0 && actualCardGO == null)
        {
            GameMemory.gamePassed = true;
            return true;
        }

        return false;
    }

    bool endGameSession() {
        DateTime localTime = DateTime.Now;
        timerTmp.text = ((finishTime.Hour - DateTime.Now.Hour) * 60 + finishTime.Minute - DateTime.Now.Minute).ToString() + ":" + (60 - DateTime.Now.Second);
        //Debug.Log((((fisishTime.Hour - DateTime.Now.Hour) * 60 + fisishTime.Minute - DateTime.Now.Minute) * 60 + (60 - DateTime.Now.Second)));
        sliderTime.value = (((finishTime.Hour - DateTime.Now.Hour) * 60f + finishTime.Minute - DateTime.Now.Minute)*60f + (60f - DateTime.Now.Second))/(minSesionDuration*60f);
        Debug.Log(finishTime.Minute + " ," + DateTime.Now.Minute);
        if (localTime.Year > finishTime.Year)
            return true;
        else if (localTime.Year < finishTime.Year)
            return false;

        if (localTime.Month > finishTime.Month)
            return true;
        else if (localTime.Month < finishTime.Month)
            return false;

        if (localTime.Day > finishTime.Day)
            return true;
        else if (localTime.Day < finishTime.Day)
            return false;

        if (localTime.Hour > finishTime.Hour)
            return true;
        else if (localTime.Hour < finishTime.Hour)
            return false;
        
        if (localTime.Minute > finishTime.Minute)
            return true;
        else if (localTime.Minute < finishTime.Minute)
            return false;

        return false;
    }

    void sendGame() {

        userGame u = new userGame();
        RestClient.Post("https://caminodelemprendedor-7b0a7-default-rtdb.europe-west1.firebasedatabase.app/.json",u);
    }
}
