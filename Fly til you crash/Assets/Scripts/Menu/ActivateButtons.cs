using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButtons : MonoBehaviour
{

    public GameObject buttons;

    void Start()
    {
        buttons.SetActive(false);
    }
}
