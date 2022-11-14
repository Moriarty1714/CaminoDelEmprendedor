using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Collections;


public class HandleCSVFile
{
    public Dictionary<int, string[]> cardsInfo = new Dictionary<int, string[]>();
    public void ReadFile()
    {
        string[] info = Resources.Load<TextAsset>("16Cards").text.Split('\n');       

        for (int i = 0; i < info.Length-1; i++)
        {
            string[] tmpInfo = info[i].Split('\t');
            cardsInfo[i] = tmpInfo;
            Debug.Log("L1: " + cardsInfo[i].GetValue(4) + ", R1: " + cardsInfo[i].GetValue(8));
            Debug.Log("L2: " + cardsInfo[i].GetValue(4) + ", R2: " + cardsInfo[i].GetValue(8));
        }


    }

}
