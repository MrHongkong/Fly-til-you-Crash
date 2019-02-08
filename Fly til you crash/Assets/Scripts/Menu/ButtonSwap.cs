using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwap : MonoBehaviour
{
    public GameObject[] buttonList;

    private void Start()
    {
        foreach(GameObject button in buttonList)
        {
            if (button.name == "Highscore") button.SetActive(true);
            else button.SetActive(false);
        }
    }
}
