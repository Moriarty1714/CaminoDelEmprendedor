using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Manager {
    AYUDANTE,
    CONTABLE,
    JEFE_DE_TIENDA,
    ALDEANO,
    HOMBRE_MISTERIOSO,
    AVENTURERO,
    LIQUIDO,
    PISTOLA,
    DEFAULT
}
public enum Affection { VERY_POSITIVE, POSITIVE, NEUTRAL, NEGATIVE, VERY_NEGATIVE}
public enum Agent { TEAM, MONEY, CLIENT, NATURE}

[SerializeField]
public class Card
{
    public Affection[] affectionsLeft = new Affection[4];
    public Affection[] affectionsRight = new Affection[4];

    public Manager manager;
    public Sprite managerImg;

    public string sentence;
    public string desicionLeft;
    public string desicionRight;

    public Card(Affection[] _leftAffections, Affection[] _rightAffections,
        Sprite _managerImg,
        Manager _manager = Manager.DEFAULT,
        string _sentence = "",
        string _desicionLeft = "",
        string _desicionRight = ""
        )
    {
        manager = _manager;
        managerImg = _managerImg;

        sentence = _sentence;
        desicionLeft = _desicionLeft;
        desicionRight = _desicionRight;

        affectionsLeft = _leftAffections;
        affectionsRight = _rightAffections;
    }

    //Gets
    public string GetManager()
    {
        switch (manager)
        {
            case Manager.AYUDANTE:
               return "AYUDANTE";
            case Manager.CONTABLE:
                return "CONTABLE";
            case Manager.JEFE_DE_TIENDA:
                return "RESPONSABLE DE TIENDA";
            case Manager.ALDEANO:
                return "ALDEANO";
            case Manager.HOMBRE_MISTERIOSO:
                return "HOMBRE MISTERIOSO";
            case Manager.AVENTURERO:
                return "AVENTURERO";
            case Manager.LIQUIDO:
                return "ITEM";
            case Manager.PISTOLA:
                return "ITEM";
            case Manager.DEFAULT:
                return "PERSONA";
            default:
                return "Un random ha aparecido!";
        }
    }
    public string GetSentence()
    {
        return sentence;
    }
    public string GetLeftDesicion()
    {
        return desicionLeft;
    }
    public string GetRightDesicion()
    {
        return desicionRight;
    }

    //Affections
    public Affection GetAffection(bool _isLeftDesicion, Agent _agentAffected)
    {

        if (_isLeftDesicion)
        {
            return affectionsLeft[(int)_agentAffected];
        }
        return affectionsRight[(int)_agentAffected];
    }
}

public class CardAndDesicion {
    Card card;
    bool isLeftDesicion;

    public CardAndDesicion(Card _card, bool _isLeftDesicion) {
        card = _card;
        isLeftDesicion = _isLeftDesicion;
    }
}
