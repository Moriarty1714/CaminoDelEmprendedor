using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMemory
{

    public static string userName;
    public static string email;
    public static string business;

    //Métricas
    public static float [] powers = new float [4];

    public static int totalCards = 36;
    public static int cardCount = 0;
    public static bool gamePassed = false;

    //Desicions
    //public static Queue<CardAndDesicion> desicions = new Queue<CardAndDesicion>();

}
public class userGame{
    public string userName;
    public string email;
    public string business;

    //Métricas
    public float[] powers = new float[4];

    public int cardCount = 0;
    public bool gamePassed = false;

    public userGame() {
        userName = GameMemory.userName;
        email = GameMemory.email;
        business = GameMemory.business;

        powers[0] = (int)(GameMemory.powers[0] * 100);
        powers[1] = (int)(GameMemory.powers[1] * 100);
        powers[2] = (int)(GameMemory.powers[2] * 100);
        powers[3] = (int)(GameMemory.powers[3] * 100);

        cardCount = GameMemory.cardCount;
        gamePassed = GameMemory.gamePassed;
    }
}