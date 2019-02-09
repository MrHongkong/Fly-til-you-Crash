using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Made by Jocke and Adam
public class FanRotation : MonoBehaviour
{
    Vector3 rotation;
    float speed;
    bool movingFan;

    private void Start()
    {
        speed = 0;
        rotation = new Vector3(0, 0, speed);

        if (Random.Range(0, 1f) < 0.5f)
            movingFan = true;
        else
            movingFan = false;
    }

    private void FixedUpdate()
    {
        if(movingFan)
        {
            speed += 0.0006f;
            rotation.z = speed;
            transform.eulerAngles = transform.eulerAngles + rotation * 1 / Time.timeScale;
            if (speed > 360) speed = 0;
        }
    }
}
