using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene("MVP");
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) SceneManager.LoadScene("MVP");
    }
    
    public void MouseClick()
    {
        SceneManager.LoadScene("MVP");
    }
}
