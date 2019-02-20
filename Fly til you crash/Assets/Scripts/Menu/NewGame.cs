using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Made by Jocke
public class NewGame : MonoBehaviour
{
    public GameObject newGame;

    private void Update()
    {
        if (Input.GetButtonDown("MenuEnter"))
        {
            iTween.PunchScale(newGame, new Vector3(2, 2, 2), 0.4f);
            Invoke("Delay", 0.5f);
        }
    }
    
    public void MouseClick()
    {
        iTween.PunchScale(newGame, new Vector3(2, 2, 2), 0.4f);
        Invoke("Delay", 0.5f);
    }

    void Delay()
    {
        SceneManager.LoadScene("Game");
    }
}
