using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMenu : MonoBehaviour
{
    public GameObject buttons;
    public GameObject highScoreTextField;


    void Start()
    {
        buttons.SetActive(false);
        highScoreTextField.SetActive(false);
    }
}
