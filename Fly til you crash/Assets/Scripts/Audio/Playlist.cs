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
            AudioManager.instance.Stop("MenuMusic");
            AudioManager.instance.Play("GameMusic");
        }

        if (sceneName == "ScoreScreen")
        {
            AudioManager.instance.Play("MenuMusic");
            AudioManager.instance.Stop("GameMusic");
        }
    }


}
