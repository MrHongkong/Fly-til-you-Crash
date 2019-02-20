using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuSettings : MonoBehaviour
{
    public bool invertedControls;
    public bool carInUse;
    public static MenuSettings instance;

    void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    
    public static void setInvertedControls(bool controls) {instance.invertedControls = controls;}
    public static void setCarInUse(bool car) {instance.carInUse = car;}

    public static bool getInvertedControls() {return instance.invertedControls;}
    public static bool getCarInUse() {return instance.carInUse;}
}
