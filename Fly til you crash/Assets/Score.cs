using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Jocke
public class Score : MonoBehaviour
{
    public GameObject score;
    public static float finalScore;
 
    private void Awake()
    {
        DontDestroyOnLoad(score);
    }
}
