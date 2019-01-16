using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Made by Jocke
public class Movement : MonoBehaviour
{
    Rigidbody rb;
    Vector3 movement;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v, speed);
    }

    void Move(float h, float v, float s)
    {
        movement.Set(h, v, 0f);
        movement = movement.normalized * s * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
}
