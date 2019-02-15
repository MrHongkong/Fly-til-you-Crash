using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public AnimationCurve difficultyCurve;

    public static Difficulty difficulty;

    // Start is called before the first frame update
    void Start(){
        if (difficulty == null)
            difficulty = this;
        else
            Destroy(this);
    }

    public static float difficultyProgress(){return difficulty.difficultyCurve.Evaluate(Time.timeSinceLevelLoad);}

    public void Update()
    {
        //Logger.Log(Difficulty.difficultyProgress());
        //Debug.Log(Difficulty.difficultyProgress());
    }
}
