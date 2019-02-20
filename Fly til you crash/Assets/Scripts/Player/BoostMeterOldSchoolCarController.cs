using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostMeterOldSchoolCarController : MonoBehaviour
{
    Color color = new Color(0f, 200f, 0f);
    public Material lightingMaterial;
    public GameObject obj;

    public void Start()
    {
        lightingMaterial = new Material(lightingMaterial);
        obj.GetComponent<Renderer>().material = lightingMaterial;
    }

    // Update is called once per frame
    void Update(){
        lightingMaterial.SetColor("_EmissionColor", color * PlayerController.playerController.GetPercentageBoost() * 0.015f);
    }
}
