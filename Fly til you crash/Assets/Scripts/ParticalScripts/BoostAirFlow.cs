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
    public GameObject BoostParticalSmall;
    private ParticleSystem part;
    float trump = 6;

    void Start()
    {
        BoostPartical.SetActive(false);
        BoostParticalSmall.SetActive(true);

        part = BoostParticalSmall.GetComponent<ParticleSystem>();
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
