using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Username : MonoBehaviour
{
    public string username = "";
    public float score;
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        score = timer.score;
    }

    public void OnClick()
    {
        username = GetComponent<InputField>().text;
        Debug.Log("" + username);
        Highscores.AddNewHighscore(username, (int)score);
    }

}
