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

    public void Update()
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
                    else if (MenuVideoController.isPause && button.name == field.name)
                    {
                        field.SetActive(true);
                    }
                }
            }
        }    
    }
}
