using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sliderTeam;
    public TextMeshProUGUI teamPercent;
    public Slider sliderMoney;
    public TextMeshProUGUI moneyPercent;
    public Slider sliderClient;
    public TextMeshProUGUI clientPercent;
    public Slider sliderNature;
    public TextMeshProUGUI naturePercent;

    public TextMeshProUGUI sentence;

    public Slider sliderCard;
    public TextMeshProUGUI numCard;

    private bool powersUpdated = false;
    void Start()
    {
        setPowerValues();
        setCardValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (!powersUpdated) {
            setPowerValues();
            setSentence();
            powersUpdated = true;
        }
    }

    void setPowerValues() {
        sliderTeam.GetComponent<AgentSlider>().nextValue = GameMemory.powers[0];
        sliderMoney.GetComponent<AgentSlider>().nextValue = GameMemory.powers[1];
        sliderClient.GetComponent<AgentSlider>().nextValue = GameMemory.powers[2];
        sliderNature.GetComponent<AgentSlider>().nextValue = GameMemory.powers[3];

        teamPercent.text = ((int)(GameMemory.powers[0] * 100)).ToString() +  " %";
        moneyPercent.text = ((int)(GameMemory.powers[1]*100)).ToString() + " %";
        clientPercent.text = ((int)(GameMemory.powers[2]*100)).ToString() + " %";
        naturePercent.text = ((int)(GameMemory.powers[3]*100)).ToString() + " %";
    }

    void setSentence() {
        if (GameMemory.powers[0] <= 0)
            sentence.text = "El equipo es el alma de la empresa, hacen posible la producci�n y los servicios";
        else if (GameMemory.powers[1] <= 0)
            sentence.text = "El dinero nos permiten la viabilidad, permanencia y bienestar del proyecto";
        else if (GameMemory.powers[2] <= 0)
            sentence.text = "Las necesidades de la clientela son nuestra raz�n de ser, sin ventas ni comunidad no hay negocio";
        else if (GameMemory.powers[3] <= 0)
            sentence.text = "La sostenibilidad nos garantizan un futuro equilibrado a nivel econ�mico, el ambiental y social.";
        else
            sentence.text = "Los valores empresariales nos gu�an a lo largo de todas nuestras acciones y decisiones. Individual y colectivamente, nos ayudan a avanzar como sociedad.";
    }

    void setCardValue() 
    {
        sliderCard.value = (float)GameMemory.cardCount / (float)GameMemory.totalCards;
        numCard.text = GameMemory.cardCount.ToString()+ "/" + GameMemory.totalCards.ToString();
    }
}
