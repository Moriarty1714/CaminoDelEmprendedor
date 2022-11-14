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

    public TextMeshProUGUI tmpLeft;
    public TextMeshProUGUI tmpRight;
    
    public bool isMouseOver = false;
    void Start()
    {
        imgCard = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(0, -0.63f, 0);
        initCardPos = transform.position;
        imgCard.sprite = card.managerImg;
    }

    void Update()
    {
        //Movement
        if (Input.GetMouseButton(0) && isMouseOver)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = pos;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, initCardPos, fMovingSpeed);
        }
    }
    private void OnMouseOver()
    {
        isMouseOver = true;
    }
    private void OnMouseExit()
    {
        isMouseOver = false;
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
