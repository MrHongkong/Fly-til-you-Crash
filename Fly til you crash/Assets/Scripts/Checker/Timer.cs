using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    //Made by Philip Åkerblom GP18 Yrgo!

    float timePassed;
    float currentTime;
    float speed = 1;
    public float theTime;
    public float scoreMultiplier;
    public float timePlayed;
    float currentScore;
    public Text timeCount;
    public Text scoreCount;
    private Acceleration accelerationSpeed;
    public float score;
    private OnCollision onCollision;

    // Start is called before the first frame update
    void Start()
    {
        timePassed = currentTime;
        accelerationSpeed = FindObjectOfType<Acceleration>();
        onCollision = FindObjectOfType<OnCollision>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!onCollision.hasStartedCoroutine)
        {
            currentTime += Time.deltaTime * speed;
            UpdateScoreCount(20);
        }

        UpdateTime();
        
        
    }

    void UpdateTime()
    {

        string minutes = Mathf.Floor((currentTime % 3600)/60).ToString("00");
        string seconds = (currentTime % 60).ToString("00");
        timeCount.text = "Time: " + minutes + ":" + seconds;


    }

    void UpdateScoreCount(float scoreMultiplier)
    {
        timePlayed = Time.timeSinceLevelLoad;
        score = timePlayed * scoreMultiplier;
        currentScore = score;
        scoreCount.text = "Score: " + score.ToString("f0");
    }
}
