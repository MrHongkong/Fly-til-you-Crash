using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [SerializeField]
    AnimationCurve speedCurve;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float float_velocity = speedCurve.Evaluate(Time.time);
        rb.velocity = float_velocity * transform.forward;
    }
}
        
        

