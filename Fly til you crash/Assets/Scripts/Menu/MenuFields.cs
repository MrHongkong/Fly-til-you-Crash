using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Made by Jocke
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
                        MonoBehaviour[] components = field.GetComponentsInChildren<MonoBehaviour>();
                        foreach (MonoBehaviour c in components)
                            c.enabled = false;
                    }
                    else if (button.name == field.name && MenuVideoController.isPause)
                    {
                        field.SetActive(true);

                        MonoBehaviour[] components = field.GetComponentsInChildren<MonoBehaviour>();
                        foreach (MonoBehaviour c in components)
                            c.enabled = true;
                    }
                }
            }
        }    
    }
}
