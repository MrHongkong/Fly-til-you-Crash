﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Made by Jocke
public class UIController : MonoBehaviour
{
    public GameObject highscoreUI;
    public GameObject userInputUI;
    public Button submit;
    public Button retry;
    public Button menu;
    public TMP_InputField inputField;
    public TextMeshProUGUI ScoreText;
    public DisplayHighscores displayHighscores;
    private string username;

    private string controllerButton = "Game";


    private void Start()
    {
        highscoreUI.SetActive(false);
        userInputUI.SetActive(true);
        ScoreText.text = "Score: " + (int)Score.finalScore;
    }
    private void Update()
    {
        if (inputField.text == "")
        {
            submit.interactable = false;
        }
        else
        {
            submit.interactable = true;
        }

        if (Input.GetButtonDown("MenuLeft"))
        {
            controllerButton = "Game";
        }
        if(Input.GetButtonDown("MenuRight"))
        {
            controllerButton = "Menu";
        }
        if(Input.GetButtonDown("MenuEnter"))
        {
            SceneManager.LoadScene(controllerButton);
        }
    }

    public void OnClickSubmit()
    {
        username = inputField.text;
        UniqID.GetUniqueID(username);
        highscoreUI.SetActive(true);
        userInputUI.SetActive(false);
        Highscores.AddNewHighscore(username, (int)Score.finalScore);
        Highscores.writeToTMProFields(displayHighscores.highscoreFields);
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
