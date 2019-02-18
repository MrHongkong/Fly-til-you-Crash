using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxDifficulty : MonoBehaviour
{
    [Range(0f, 1f)]
    public float min;
    [Range(0f, 1f)]
    public float max;

    // Start is called before the first frame update
    void Start(){
        if (DifficultyController.DifficultyProgress() < min || DifficultyController.DifficultyProgress() > max){
            Destroy(gameObject);
            if (DifficultyController.DifficultyProgress() < min)
                Debug.Log("Current difficulty too low, despawning!");
            else
                Debug.Log("Current difficulty too high, despawning!");
        }
    }
}
