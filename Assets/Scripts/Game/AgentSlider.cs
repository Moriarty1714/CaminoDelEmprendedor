using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentSlider : MonoBehaviour
{
    public Slider sliderAgent;
    public Agent agentType;

    public float lastValue = 0;
    public float nextValue = 0;
    public float lastChange = 0;
    public float duration = 0f;

    public Image fillImg;
    public Color defaultFillColor;

    // Start is called before the first frame update
    void Start()
    {
        sliderAgent = GetComponent<Slider>();
        nextValue = sliderAgent.value;
        lastValue = 0.001f;
        lastChange = Time.time;

        duration = Mathf.Abs(nextValue - lastValue) * 2;
        defaultFillColor = fillImg.color;
    }

    // Update is called once per frame
    void Update()
    {
        //Poner un condicional de control para que no haga el calculo en cada loop
        sliderAgent.value = Mathf.Lerp(lastValue, nextValue, (Time.time - lastChange) / duration);

        if (sliderAgent.value == nextValue && lastValue != sliderAgent.value)
        {
            lastValue = sliderAgent.value;
            fillImg.color = defaultFillColor;
        }
        else if (lastValue > nextValue)
        {
            fillImg.color = Color.red;
        }
        else if (lastValue < nextValue)
        {
            fillImg.color = Color.green;
        }

    }

    public void updateAgent(Affection _a) {
        if (!(nextValue <= 0))
        {
            lastChange = Time.time;
            lastValue = sliderAgent.value;
            switch (_a)
            {
                case Affection.VERY_POSITIVE:
                    nextValue += 0.20f;
                    break;
                case Affection.POSITIVE:
                    nextValue += 0.10f;
                    break;
                case Affection.NEGATIVE:
                    nextValue -= 0.10f;
                    break;
                case Affection.VERY_NEGATIVE:
                    nextValue -= 0.20f;
                    break;
                default:
                    break;
            }
            duration = Mathf.Abs(nextValue - lastValue) * 2;

            if (nextValue > 1)
                nextValue = 1f;

            //Parche provisional error de sumar y restar valores decimales a sliderAgent
            if (nextValue < 0.01)
                nextValue = 0f;

        }
    }
}
