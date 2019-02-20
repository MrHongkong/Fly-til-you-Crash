using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSettings : MonoBehaviour
{
    public static bool invertedControls = true;
    public MenuSettings menuSetting;
    private void Awake()
    {
        DontDestroyOnLoad(menuSetting);
    }
    
    //public void UpdateIsOn(bool update)
    //{
    //    invertedControls = update;
    //}
}
