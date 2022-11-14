using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentSlider : MonoBehaviour
{
    public Slider sliderAgent;
    public Agent agentType;


    // Start is called before the first frame update
    void Start()
    {
        sliderAgent = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool updateAgent(Affection _a) {
        switch (_a)
        {
            case Affection.VERY_POSITIVE:
                sliderAgent.value += 0.2f;
                break;
            case Affection.POSITIVE:
                sliderAgent.value += 0.1f;
                break;
            case Affection.NEGATIVE:
                sliderAgent.value -= 0.1f;
                break;
            case Affection.VERY_NEGATIVE:
                sliderAgent.value -= 0.2f;
                break;
            default:
                break;
        }

        if (sliderAgent.value > 1)
            sliderAgent.value = 1f;

        //Parche provisional error de bumar y restar valores decimales a sliderAgent
        if (sliderAgent.value < 0.01)
            sliderAgent.value = 0f;

        return sliderAgent.value <= 0;
    }
}
