using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostMeterOldSchoolCarController : MonoBehaviour
{
    public Transform boostMeterCube;

    float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        maxScale = boostMeterCube.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scales = boostMeterCube.localScale;
        scales.y = Mathf.Lerp(scales.y, PlayerController.playerController.GetPercentageBoost() * maxScale < 0.001f ? 0f : PlayerController.playerController.GetPercentageBoost() * maxScale, 0.2f);
        boostMeterCube.localScale = scales;
    }
}
