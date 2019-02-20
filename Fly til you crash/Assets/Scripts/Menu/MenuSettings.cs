using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    public static bool isOn;
    public MenuSettings menuSetting;
    private void Awake()
    {
        DontDestroyOnLoad(menuSetting);
    }
    
    public void UpdateIsOn(bool update)
    {
        isOn = update;
    }
}
