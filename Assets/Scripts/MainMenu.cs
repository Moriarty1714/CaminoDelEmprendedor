using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Image ludensLogo;
    public Image acehLogo;
    public Image splashScreenPanel;

    public TextMeshProUGUI nameUser;
    public TextMeshProUGUI email;
    public TextMeshProUGUI business;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeImage(ludensLogo,0, false));
        StartCoroutine(FadeImage(acehLogo, 0, false));
        StartCoroutine(FadeImage(ludensLogo, 2, true));
        StartCoroutine(FadeImage(acehLogo, 2, true));
        StartCoroutine(FadeImage(splashScreenPanel,3, true));
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

    IEnumerator FadeImage(Image img,int seconds, bool fadeAway)
    {
        yield return new WaitForSeconds(seconds);
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(img.color.r, img.color.g, img.color.b, i);
                yield return null;
            }
            Destroy(img.gameObject);
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(img.color.r, img.color.g, img.color.b, i);
                yield return null;
            }
        }
    }
}
