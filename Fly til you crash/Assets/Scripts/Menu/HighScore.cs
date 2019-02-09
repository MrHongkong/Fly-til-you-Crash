using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public GameObject highScoreTextField;
    public GameObject highScoreButton;
    public bool isOn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) OnClick();
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) OnClick();    
    }


    public void OnClick()
    {
        isOn = !isOn;
        highScoreTextField.SetActive(isOn);
        iTween.PunchScale(highScoreButton, new Vector3(2, 2, 2), 0.4f);
    }
}
