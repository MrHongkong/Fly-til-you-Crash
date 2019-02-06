using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotation : MonoBehaviour
{
    Vector3 rotationZ;
    float incZ;

    bool movingFan;

    private void Start()
    {
        incZ = 0;
        rotationZ = new Vector3(0, 0, incZ);

        if (Random.Range(0, 1f) < 0.5f)
            movingFan = true;
        else
            movingFan = false;
    }

    private void FixedUpdate()
    {
        if(movingFan)
        {
            incZ += 0.0006f;
            rotationZ.z = incZ;
            timer = Time.timeSinceLevelLoad + 0.5f;
            transform.eulerAngles = transform.eulerAngles + rotationZ * 1 / Time.timeScale;

            if (incZ > 360) incZ = 0;
        }
    }
}
