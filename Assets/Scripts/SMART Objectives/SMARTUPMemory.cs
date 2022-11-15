using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SMARTUPMemory
{
    public static string userID;
    public static string userName;
    public static string email;
    public static string business;

    //Métricas
    public static float [] powers = new float [4];

    public static int totalCards = 0;
    public static int cardCount = 0;
    public static bool gamePassed = false;

    //Desicions
    //public static Queue<CardAndDesicion> desicions = new Queue<CardAndDesicion>();

}
