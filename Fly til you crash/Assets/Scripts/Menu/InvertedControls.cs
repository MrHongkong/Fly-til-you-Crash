using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InvertedControls : MonoBehaviour
{
    public Toggle toggle;
    public MenuSettings menuSettings;
    public void OnClick()
    {
        if (toggle.isOn) menuSettings.UpdatePlayerController(false);
        else menuSettings.UpdatePlayerController(true);
    }
}
