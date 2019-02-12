using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public GameObject highscoreUI;
    public GameObject userInputUI;
    public GameObject scoreUI;
    public GameObject buttons;
    public Button submit;
    public Button retry;
    public Button menu;
    public RawImage backgroundImage;
    public TMP_InputField inputField;
    public TextMeshProUGUI inputeScoreText;
    public static int score;
    private string username;


    private void Start()
    {
        scoreUI.SetActive(true);
        highscoreUI.SetActive(false);
        userInputUI.SetActive(false);
        buttons.SetActive(false);
        backgroundImage.enabled = false;
    }

    public void OnClickSubmit()
    {
        username = inputField.text;
        UniqID.GetUniqueID(username);
        highscoreUI.SetActive(true);
        buttons.SetActive(true);
        userInputUI.SetActive(false);
        Highscores.AddNewHighscore(username, score);
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene("MVP");
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
