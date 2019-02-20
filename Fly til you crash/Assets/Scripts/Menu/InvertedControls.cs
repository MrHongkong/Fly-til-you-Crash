using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InvertedControls : MonoBehaviour
{
    public Toggle toggle;
    public MenuSettings menuSettings;

    private void Start()
    {
        toggle.isOn = MenuSettings.invertedControls;
    }

    public void Whenchanged()
    {
        //if (toggle.isOn) menuSettings.UpdateIsOn(false);
        //else menuSettings.UpdateIsOn(true);
        MenuSettings.invertedControls = toggle.isOn;

    }
}
