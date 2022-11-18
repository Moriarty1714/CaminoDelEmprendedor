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
            GameMemory.userName = nameUser.text;
        else
            GameMemory.userName = "Unknowed";

        if (email.text.ToString().Length != 1)
            GameMemory.email = email.text;
        else
            GameMemory.email = "unknowed@unknowed.com";

        if (business.text.ToString().Length != 1)
            GameMemory.business = business.text;
        else
            GameMemory.business = "UnknowedSL";
    }

    public void sceneGame() {
        SceneManager.LoadScene("Game");
    }
}
