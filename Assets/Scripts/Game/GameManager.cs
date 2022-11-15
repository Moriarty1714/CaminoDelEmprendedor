using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    bool endGame;
    void Start()
    {
        endGame = false;
        cards = new Stack<Card>();
        reader.ReadFile();
        //generateStackTest();
        generateStack();
    }

    // Update is called once per frame
    void Update()
    {
        if (cards.Count > 0 && !endGame)
        {
            if (actualCardGO == null)
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

                        SMARTUPMemory.cardCount++;
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

                        SMARTUPMemory.cardCount++;
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
            SMARTUPMemory.powers = getSlidersValue();
            SceneManager.LoadScene("GameOver");
        }
    }

    void UpdateAgents(bool _isLeftDesicion = false)
    {
        if (teamAgent.updateAgent(actualCardCL.GetAffection(_isLeftDesicion, teamAgent.agentType)) ||
        moneyAgent.updateAgent(actualCardCL.GetAffection(_isLeftDesicion, moneyAgent.agentType)) ||
        clientAgent.updateAgent(actualCardCL.GetAffection(_isLeftDesicion, clientAgent.agentType)) ||
        natureAgent.updateAgent(actualCardCL.GetAffection(_isLeftDesicion, natureAgent.agentType)))
            endGame = true;
    }

    float[] getSlidersValue()
    {
        float[] tmp = new float[4];
        tmp[0] = teamAgent.sliderAgent.value;
        tmp[1] = moneyAgent.sliderAgent.value;
        tmp[2] = clientAgent.sliderAgent.value;
        tmp[3] = natureAgent.sliderAgent.value;

        return tmp;
    }

    //void generateStackTest()
    //{
    //    Card tmp;
    //    Affection[] affectionsLeft = new Affection[4];
    //    Affection[] affectionsRight = new Affection[4];
    //    for (int i = 0; i < 20; i++)
    //    {
    //        for (int j = 0; j < affectionsLeft.Length - 1; j++)
    //        {
    //            affectionsLeft[j] = (Affection)Random.Range(0, 5);
    //            affectionsRight[j] = (Affection)Random.Range(0, 5);
    //        }
    //        tmp = new Card(affectionsLeft, affectionsRight, (Manager)Random.Range(0, 6), "Esta carta es la número " + (i + 1).ToString(), "Decisión de la izquierda!" + (i + 1), "Desición de la derecha!" + (i + 1));
    //        cards.Push(tmp);
    //    }
    //}
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

        SMARTUPMemory.totalCards = cards.Count;
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
}
