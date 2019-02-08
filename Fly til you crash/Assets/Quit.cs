using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) Application.Quit();
        if(Input.GetKeyDown(KeyCode.Joystick1Button0)) Application.Quit();
    }

    public void MouseClick()
    {
        Application.Quit();
    }
}
