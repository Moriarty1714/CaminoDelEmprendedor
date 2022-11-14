using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI nameUser;
    public TextMeshProUGUI codeSession;

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

        if (codeSession.text.ToString().Length != 1)
            SMARTUPMemory.sesionCode = codeSession.text;
        else
            SMARTUPMemory.sesionCode = "-1";
    }
}
