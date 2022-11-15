using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI nameUser;
    public TextMeshProUGUI email;
    public TextMeshProUGUI business;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveInfo() {
        if (nameUser.text.ToString().Length != 1)
            SMARTUPMemory.userName = nameUser.text;
        else
            SMARTUPMemory.userName = "Unknowed";

        if (email.text.ToString().Length != 1)
            SMARTUPMemory.email = email.text;
        else
            SMARTUPMemory.email = "unknowed@unknowed.com";

        if (business.text.ToString().Length != 1)
            SMARTUPMemory.business = business.text;
        else
            SMARTUPMemory.business = "UnknowedSL";
    }

    public void sceneGame() {
        SceneManager.LoadScene("Game");
    }
}
