using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public GameObject newGame;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) StartCoroutine(Delay());
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) StartCoroutine(Delay());
    }
    
    public void MouseClick()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        SceneManager.LoadScene("MVP");
        iTween.PunchScale(newGame, new Vector3(2,2,2), 0.4f);
        yield return new WaitForSeconds(1);
    }
}
