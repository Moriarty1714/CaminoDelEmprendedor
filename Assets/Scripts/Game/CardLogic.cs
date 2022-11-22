using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public bool isMouseOver = false;
    void Start()
    {
        imgCard = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(0, -0.3199f, 0);
        initCardPos = transform.position;
        imgCard.sprite = card.managerImg;

    }

    void Update()
    {
        //Movement
        if (Input.GetMouseButtonDown(0))
        {
            initTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        else if (Input.GetMouseButton(0) && isMouseOver)
        {
            Vector2 dragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = initCardPos - (initTouchPos - dragPos);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, initCardPos, fMovingSpeed);
        }

        float angle = (-20 * transform.position.x) / 1.5f;
        if (angle <= 20 && angle >= -20)
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
    private void OnMouseOver()
    {
        isMouseOver = true;
    }
    private void OnMouseExit()
    {
        isMouseOver = false;
        initTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
