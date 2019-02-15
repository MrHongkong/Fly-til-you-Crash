using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playlist : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;


        if (sceneName == "Game")
        {
            FindObjectOfType<AudioManager>().Stop("MenuMusic");
            FindObjectOfType<AudioManager>().Play("GameMusic");
        }

        if (sceneName == "ScoreScreen")
        {
            FindObjectOfType<AudioManager>().Play("MenuMusic");
            FindObjectOfType<AudioManager>().Stop("GameMusic");
        }
    }


}
