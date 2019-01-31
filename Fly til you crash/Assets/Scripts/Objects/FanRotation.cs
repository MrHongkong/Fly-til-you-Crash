using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotation : MonoBehaviour
{
    Vector3 rotationZ;
    float timer;
    float incZ;

    private void Start()
    {
        incZ = 0;
        rotationZ = new Vector3(0, 0, incZ);
    }

    private void Update()
    {
        incZ += 0.0006f;
        rotationZ.z = incZ;
        timer = Time.timeSinceLevelLoad + 0.5f;
        transform.eulerAngles = transform.eulerAngles + rotationZ;

        if (incZ > 360) incZ = 0;
    }
}
