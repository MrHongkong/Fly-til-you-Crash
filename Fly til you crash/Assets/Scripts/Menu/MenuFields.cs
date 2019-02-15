using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFields : MonoBehaviour
{
    public GameObject[] fields;
    public GameObject[] buttons;
    public GameObject buttonParent;

    void Start()
    {
        buttonParent.SetActive(false);
        foreach (GameObject field in fields)
        {
            field.SetActive(false);
        }
    }

    private void Update()
    { 
        foreach (GameObject field in fields)
        {
            foreach(GameObject button in buttons)
            {
                if (button.activeInHierarchy)
                {
                    if (button.name != field.name)
                    {
                        field.SetActive(false);
                    }
                    else if (button.name == field.name && MenuVideoController.isPlause) field.SetActive(true);
                }
            }
        }
    }
}
