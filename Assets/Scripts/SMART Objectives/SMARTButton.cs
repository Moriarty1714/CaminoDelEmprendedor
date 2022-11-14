using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SMARTButton : MonoBehaviour
{

    public void Update()
    {

    }
    public void onClick() { 
        SMARTUPMemory.SMARTObjChoosed = GetComponentInChildren<Text>().text;
        SceneManager.LoadScene("Game");
    }


}

