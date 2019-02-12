using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    public TextMeshProUGUI scoreCount;
    private OnCollision onCollision;
    private float score;
    private float extraScore;
    private float scoreMultiplier;

    void Start()
    {
        onCollision = FindObjectOfType<OnCollision>();
        ScoreMultiplier(20);
        extraScore = 0;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) ExtraScore(1000);
        if (!onCollision.hasStartedCoroutine)
        {
            CalculateScore();
            UpdateScore();
        }
        else UIController.score = (int)score;
    }

    private void UpdateScore()
    {
        scoreCount.text = "Score: " + score.ToString("f0");
    }
    
    private void CalculateScore()
    {
        float timeScore = Time.timeSinceLevelLoad * scoreMultiplier;
        score = timeScore + extraScore;
    }

    public void ExtraScore(float bonusScore)
    {
        extraScore += bonusScore;
    }

    public void ScoreMultiplier(float multiplier)
    {
        scoreMultiplier = multiplier;
    }

    public float GetScore()
    {
        return score;
    }
}
