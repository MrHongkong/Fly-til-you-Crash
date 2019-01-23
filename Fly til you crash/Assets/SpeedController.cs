using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float MaxSpeed;
    public float AccelerationSpeed;

    float e = 2.7182818284590452353602874713527f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float float_velocity = MaxSpeed - MaxSpeed / Mathf.Pow((e), AccelerationSpeed * Time.time);
        Camera.main.fieldOfView = Mathf.Clamp(float_velocity, 80f, 120f);
        rb.velocity = float_velocity * transform.forward;

    }
}
