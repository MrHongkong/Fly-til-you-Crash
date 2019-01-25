using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Username : MonoBehaviour
{
    //Made by Philip Åkerblom GP18 Yrgo
    public string username = "";
    public float score;
    public GameObject HighscoreUI;
    public GameObject UserInputUI;
    Timer timer;
    OnCollision collision;

    void Start()
    {
        HighscoreUI.SetActive(false);
        UserInputUI.SetActive(true);
        timer = FindObjectOfType<Timer>();
        collision = FindObjectOfType<OnCollision>();

    }

    void Update()
    {
        score = timer.score;

    }

    public void OnClick()
    {
        username = GetComponent<InputField>().text;
        HighscoreUI.SetActive(true);
        UserInputUI.SetActive(false);
        Highscores.AddNewHighscore(username, (int)score);
    }

    public void OnClickRestart()
    {
        collision.ReloadGame();
        HighscoreUI.SetActive(false);
        UserInputUI.SetActive(false);
        score = timer.score = 0;
        score = timer.timePlayed = 0;
        score = timer.scoreMultiplier = 0;
        score = timer.theTime = 0;
        Time.timeScale = 1;
    }

}
