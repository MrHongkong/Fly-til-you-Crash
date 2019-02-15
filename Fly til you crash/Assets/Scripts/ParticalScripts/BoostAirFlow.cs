using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostAirFlow : MonoBehaviour
{
    public GameObject BoostPartical;

    void Start()
    {
        BoostPartical.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButton("Fastmotion"))
        {
           BoostPartical.SetActive(true);
        }
        else
        {
           BoostPartical.SetActive(false);
        }
    }   
}
