using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creatred by: ??
/// adjustments: Oscar Oders 
/// </summary>

public class BoostAirFlow : MonoBehaviour
{
    
    public GameObject BoostPartical;
    private ParticleSystem part;
    float trump = 6;

    void Start()
    {
        BoostPartical.SetActive(false);
    }

    void Update()
    {
        trump += (trump < 50) ? 0.001f : 0;
        
        if (Input.GetButton("Slowmotion")) {
            part.emissionRate = 0;
        } else {
            part.emissionRate = trump;
        }

        if (Input.GetButton("Fastmotion")) {
            BoostPartical.SetActive(true);
        } else {
            BoostPartical.SetActive(false);
        }
    }   
}
