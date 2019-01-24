using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    float constSpeed = 20;
    float AccelerationSpeed = 0.015f;
    float secondsToSpeedIncrease;
    float e = 2.7182818284590452353602874713527f;
    float speed;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(Time.time > secondsToSpeedIncrease && Time.time < 300)
        {
            secondsToSpeedIncrease = Time.time + 1;
            speed = Mathf.Pow((e), AccelerationSpeed * Time.time);
        }
        float float_velocity = constSpeed + speed;
        Camera.main.fieldOfView = Mathf.Clamp(float_velocity, 80f, 120f);
        rb.velocity = float_velocity * transform.forward;

    }
}
