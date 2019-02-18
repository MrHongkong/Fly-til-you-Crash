using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public AnimationCurve difficultyCurve;
    public float currentDifficulty = 0f;
    public static DifficultyController difficulty;

    // Start is called before the first frame update
    void Start(){
        if (difficulty == null)
            difficulty = this;
        else
            Destroy(this);
    }

    public static float DifficultyProgress(){return difficulty.difficultyCurve.Evaluate(Time.timeSinceLevelLoad);}

    public void Update()
    {
        currentDifficulty = DifficultyProgress();
        //Logger.Log(Difficulty.difficultyProgress());
        //Debug.Log(Difficulty.difficultyProgress());
    }
}
