using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetButtonDown("MenuEnter"))
        {
            Application.Quit();
        }
    }

    public void MouseClick()
    {
        Application.Quit();
    }
}
