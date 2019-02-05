using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplaySystem : MonoBehaviour {

    [SerializeField] internal AnimationCurve sizeCurve;
    private float speedMultiplyer = 0.05f;
    private int tempScore, lastTens, lastHundreds, lastThousands, lastTenThousands, lastHundredThousands, lastMillions;
    TextMeshProUGUI text;
    CharAttributes cH;
    int ff;

    private void Start() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        cH = new CharAttributes(1, 2);
    }

    private void Update() {

        ff += 20;
        DisplayScore(ref ff);
    }

    internal void DisplayScore(ref int score) {
        int scoreTens = score / 10;
        int scoreHundreds = score / 100;
        int scoreThousands = score / 1000;
        int scoreTenthousands = score / 10000;
        int scoreHundredThousands = score / 100000;
        int scoreMillions = score / 1000000;
        Debug.Log("here1");

        if (scoreTens - lastTens >= 1) {
            cH.SetScore(1);
            cH.ReSize(1.4f);
            cH.size += 1;
            lastTens = scoreTens;
        }

        if (scoreHundreds - lastHundreds >= 1) {
            ScoreEffects(100);
            lastHundreds = scoreHundreds;
        }

        if (scoreThousands - lastThousands >= 1) {
            ScoreEffects(1000);
            lastThousands = scoreThousands;
        }

        if (scoreTenthousands - lastThousands >= 1) {
            ScoreEffects(10000);
            lastTenThousands = scoreTenthousands;
        }

        if (scoreHundredThousands - lastHundredThousands >= 1) {
            ScoreEffects(100000);
            lastHundredThousands = scoreHundredThousands;
        }

        if (scoreMillions - lastMillions >= 1) {
            ScoreEffects(1000000);
            lastMillions = scoreMillions;
        }
    }

    private void ScoreEffects(int scoreToAdd) {

        tempScore += scoreToAdd;
        string displayScore = "";

        if (tempScore > 10) {
            Debug.Log("here3");
            displayScore = cH.stringWithAttributes + 0;
        }

        if (tempScore > 100) {
            displayScore = cH.stringWithAttributes + displayScore;
        }

        if (tempScore > 1000) {
            displayScore = cH.stringWithAttributes + displayScore;
        }

        if (tempScore > 10000) {
            displayScore = cH.stringWithAttributes + displayScore;
        }

        if (tempScore > 100000) {
            displayScore = cH.stringWithAttributes + displayScore;
        }

        if (tempScore > 1000000) {
            displayScore = cH.stringWithAttributes + displayScore;
        }

        Debug.Log("here4");
        text.text = displayScore;
    }

    private void SizeChange(ref CharAttributes cH) {
        float curveTime = 0f;
        float curveAmount = sizeCurve.Evaluate(curveTime);

        while (curveTime < 5.0f) {

            curveTime += Time.deltaTime * speedMultiplyer;
            Debug.Log("curveTime: " + curveTime);
            curveAmount = sizeCurve.Evaluate(curveTime);
            Debug.Log("cureveAmount: " + curveAmount);
            //cH.ReSize(curveAmount);
            cH.size += 1;
            Debug.Log("cHSize" + cH.size);
            if (Input.GetKeyDown(KeyCode.M)) {
                break;
            }
        }
}
}

public struct CharAttributes {

    internal float size;
    internal int score;
    internal string stringWithAttributes;

    internal CharAttributes(float _size, int _score) {

        size = _size;
        score = _score;
        stringWithAttributes = "<size=" + size + "%>" + score + "</size>";
    }

    internal void ReSize(float timesSize) {
        Debug.Log(timesSize);
        this.size = timesSize * 100;
    }

    internal void ResetSize() {
        this.size = 1;
    }

    internal void SetScore(int amount) {
        score += amount;
    }
}