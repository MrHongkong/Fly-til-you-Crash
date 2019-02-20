using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Controls : MonoBehaviour
{
    public Toggle invertedSwitch;
    public Toggle carSwitch;

    public void OnEnable(){
        invertedSwitch.isOn = MenuSettings.getInvertedControls();
        carSwitch.isOn = MenuSettings.getCarInUse();
    }

    public void InvertedSwitch(){
        MenuSettings.setInvertedControls(invertedSwitch.isOn);
    }

    public void CarSwitch(){
        MenuSettings.setCarInUse(carSwitch.isOn);
    }
}
