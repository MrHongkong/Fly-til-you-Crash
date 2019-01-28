using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [SerializeField]
    AnimationCurve speedCurve;
    Rigidbody rb;
    public OnCollision onCollision;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!onCollision.isDead)
        {
            float float_velocity = speedCurve.Evaluate(Time.timeSinceLevelLoad);
            rb.velocity = float_velocity * transform.forward;
        }
        
    }

    public void SetSpeed(Vector3 speed)
    {
        rb.velocity = speed;
    }
}
        
        

