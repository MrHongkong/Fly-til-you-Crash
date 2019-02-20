using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostMeterController : MonoBehaviour
{
    public Transform boostMeterCube;

    float maxScale;

    // Start is called before the first frame update
    void Start(){
        maxScale = boostMeterCube.localScale.x;
    }

    // Update is called once per frame
    void Update(){
        Vector3 scales = boostMeterCube.localScale;
        scales.x = Mathf.Lerp(scales.x, PlayerController.playerController.GetPercentageBoost() * maxScale < 0.001f ? 0f : PlayerController.playerController.GetPercentageBoost() * maxScale, 0.2f);
        boostMeterCube.localScale = scales;
    }
}
