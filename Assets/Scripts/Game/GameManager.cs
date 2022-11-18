using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Proyecto26;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    [SerializeField] private Stack<Card> cards;
    private HandleCSVFile reader = new HandleCSVFile();

    GameObject actualCardGO;
    CardLogic actualCardCL;

    public TextMeshProUGUI sentence;
    public TextMeshProUGUI nameCharachter;

    public AgentSlider teamAgent;
    public AgentSlider moneyAgent;
    public AgentSlider clientAgent;
    public AgentSlider natureAgent;

    public Sprite[] managersImg = new Sprite[7];

    //Timer
    public TextMeshProUGUI timerTmp;
    
    public int dia;
    public int mes;
    public int año;

    public int hora;
    public int minuto;
    
    void Start()
    {
        cards = new Stack<Card>();
        reader.ReadFile();
        //generateStackTest();
        generateStack();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (endGameSession()) 
        {
            Debug.Log("DONE");
        }

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
                if (actualCardGO.transform.position.x > 1.5) //Right
                {
                    actualCardCL.imgCard.color = new Color(0.4f, 0.4f, 0.4f);
                    actualCardCL.RightText();
                    if (!Input.GetMouseButton(0))
                    {
                        UpdateAgents();

                        GameMemory.cardCount++;
                        Destroy(actualCardGO);
                    }
                }
                else if (actualCardGO.transform.position.x < -1.5) //Left
                {
                    actualCardCL.imgCard.color = new Color(0.4f, 0.4f, 0.4f);
                    actualCardCL.LeftText();
                    if (!Input.GetMouseButton(0))
                    {
                        UpdateAgents(true);

                        GameMemory.cardCount++;
                        Destroy(actualCardGO);
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
            SceneManager.LoadScene("GameOver");
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
        for (int i = 0; i < reader.cardsInfo.Count; i++)
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
            case "RRHH":
                return Manager.RRHH;
            case "PRODUCT":
                return Manager.PRODUCT;
            case "ADMIN":
                return Manager.ADMIN;
            case "JUNTA":
                return Manager.BOARD;
            case "MARKETING":
                return Manager.MARKETING;
            case "FINANZAS":
                return Manager.FINANCES;
            default:
                return Manager.DEFAULT;
        }
    }
    private bool endGameCondController()
    {

        //Mira si hay algun valor de los poderes a 0 para finalizar el juego.
        if (teamAgent.sliderAgent.value <= 0 || moneyAgent.sliderAgent.value <= 0 || clientAgent.sliderAgent.value <= 0 || natureAgent.sliderAgent.value <= 0)
        {
            sendGame();
            return true;
        }
        
        //si no, mira si quedan cartas en la baraja
        if (cards.Count <= 0 && actualCardGO == null)
        {
            GameMemory.gamePassed = true;
            sendGame();
            return true;
        }

        return false;
    }

    bool endGameSession() {
        DateTime localTime = DateTime.Now;
        timerTmp.text = ((hora - DateTime.Now.Hour) * 60 + minuto - DateTime.Now.Minute).ToString() + ":" + (60 - DateTime.Now.Second);
        if (localTime.Year > año)
            return true;
        else if (localTime.Year < año)
            return false;

        if (localTime.Month > mes)
            return true;
        else if (localTime.Month < mes)
            return false;

        if (localTime.Day > dia)
            return true;
        else if (localTime.Day < dia)
            return false;

        if (localTime.Hour > hora)
            return true;
        else if (localTime.Hour < hora)
            return false;
        
        if (localTime.Minute > minuto)
            return true;
        else if (localTime.Minute < minuto)
            return false;

        return false;
    }

    void sendGame() {

        userGame u = new userGame();
        Debug.Log(u.email);
        RestClient.Post("https://caminodelemprendedor-7b0a7-default-rtdb.europe-west1.firebasedatabase.app/.json",u);
    }
}
