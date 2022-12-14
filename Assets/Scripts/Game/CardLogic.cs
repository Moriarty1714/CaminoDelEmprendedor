using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardLogic : MonoBehaviour
{
    public Card card;
    public SpriteRenderer imgCard;

    public float fMovingSpeed;
    private Vector2 initCardPos;
    private Vector2 initTouchPos;

    public TextMeshProUGUI tmpLeft;
    public TextMeshProUGUI tmpRight;

    public bool isMouseDown = false;

    private Vector3 initLocalScale;

    //Tutorial
    private GameObject mouseTutorial;
    private Sprite initSprite;
    private bool isTutorialActive;
    void Start()
    {
        imgCard = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(0.001f, -0.56f, 0);
        initCardPos = transform.position;
        imgCard.sprite = card.managerImg;

        initLocalScale = gameObject.transform.localScale;

        mouseTutorial = GameObject.Find("Tutorial/TutorialPanel/Mouse");
        isTutorialActive = true;

        if (mouseTutorial != null)
        {
            initSprite = mouseTutorial.gameObject.GetComponent<Image>().sprite;
        }
        else
        {
            isTutorialActive = false;
        }

    }

    void Update()
    {
        if (isTutorialActive)
        {           
            if (initSprite != mouseTutorial.gameObject.GetComponent<Image>().sprite)
            {
                    Vector2 dragPos = mouseTutorial.gameObject.transform.position;
                    transform.position = initCardPos - (initTouchPos - dragPos);
            }
            else initTouchPos = mouseTutorial.gameObject.transform.position;
        }
        else
        {
            //Movement
            if (Input.GetMouseButtonDown(0))
            {
                initTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            }
            else if (Input.GetMouseButton(0) && isMouseDown)
            {
                Vector2 dragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = initCardPos - (initTouchPos - dragPos);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, initCardPos, fMovingSpeed);
            }
        }

        //Angle
        float angle = (-20 * transform.position.x) / 1.5f;
        if (angle <= 20 && angle >= -20)
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
    private void OnMouseEnter()
    {

    }

    private void OnMouseDown()
    {
        if (isTutorialActive)
        {
            isTutorialActive = false;
            Destroy(mouseTutorial.transform.parent.gameObject);
        }
        isMouseDown = true;
        gameObject.transform.localScale *= 1.10f;
    }
    private void OnMouseUp()
    {

        isMouseDown = false;
        initTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.localScale = initLocalScale; 
    }

    //Texts
    public void RightText()
    {
        tmpRight.text = card.GetRightDesicion();
    }
    public void LeftText()
    {
        tmpLeft.text = card.GetLeftDesicion();
    }
    public void NoneText()
    {
        tmpLeft.text = "";
        tmpRight.text = "";
    }

    //Get
    public string GetSentence() 
    {
        return card.GetSentence();
    }
    public string GetManager()
    {
        return card.GetManager();
    }
    public Affection GetAffection(bool _isLeftDesicion, Agent _agentAffected)
    {
        return card.GetAffection(_isLeftDesicion, _agentAffected);
    }
    //Set
    public void SetCard(Card _c) {
        card = _c;
    }
}
