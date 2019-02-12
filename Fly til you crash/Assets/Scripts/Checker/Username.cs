using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Username : MonoBehaviour
{
    //Made by Philip Åkerblom GP18 Yrgo

    public string username;
    public float score;
    public GameObject HighscoreUI;
    public GameObject UserInputUI;
    public Button submit;
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
        if (GetComponent<InputField>().text == "")
        {
            submit.interactable = false;
        }
        else
        {
            submit.interactable = true;
        }
    }

    String GetUniqueID(String username)
    {
       string[] split = System.Guid.NewGuid().ToString().Split(new Char[] { ':', '.' });
       string id = "";
       for (int i = 0; i < split.Length; i++)
       {
           id = username + " " + split[i];
       }
        return id;
    }
   

    public void OnClick()
    {
        username = GetComponent<InputField>().text;
        username = GetUniqueID(username);
        HighscoreUI.SetActive(true);
        UserInputUI.SetActive(false);
        Highscores.AddNewHighscore(username, (int)score);
    }

    public void OnClickRestart()
    {
        //collision.ReloadGame();
        HighscoreUI.SetActive(false);
        UserInputUI.SetActive(false);
        Time.timeScale = 1;
    }

}
