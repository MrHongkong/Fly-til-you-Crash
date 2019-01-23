using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    //Made by Philip Åkerblom GP18 Yrgo!

    float timePassed;
    float currentTime;
    float accelerationCount = 0.0f;
    float speed = 1;
    float theTime;
    public Text timeCount;
    public Text speedCount;
    private Acceleration accelerationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //timeCount = GetComponent<Text>();
        //speedCount = GetComponent<Text>();
        timePassed = currentTime;
        accelerationSpeed = FindObjectOfType<Acceleration>();

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * speed;
        UpdateTime();

        accelerationCount += accelerationSpeed.forwardVelocity;
        UpdateSpeedCount();

    }

    void UpdateTime()
    {
        string minutes = ((currentTime % 3600) / 60).ToString("00");
        string seconds = (currentTime % 60).ToString("00");
        timeCount.text = "Time: " + minutes + ":" + seconds;
        //timeCount.text = "Time: " + currentTime.ToString("f1");
        //Debug.Log("Time: " + currentTime);
    }

    void UpdateSpeedCount()
    {
        speedCount.text = "Score: " + accelerationCount.ToString("f0");
        //Debug.Log("Speed: " + accelerationCount);
    }
}
