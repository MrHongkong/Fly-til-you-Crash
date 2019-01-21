using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code by Adam
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float yAxis;
    float enginePower = 1f;
    Rigidbody rb;

    public float KeyDownDrag;
    public float KeyUpDrag;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 joystick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (joystick.magnitude < 0.1f)
            rb.angularDrag = KeyUpDrag;
        else
            rb.angularDrag = KeyDownDrag;

        //Banking controls, turning turning left and right on Z axis
        rb.AddTorque(Input.GetAxis("Horizontal") * transform.forward * -0.5f);

        //Pitch controls, turning the nose up and down
        rb.AddTorque(Input.GetAxis("Vertical") * transform.right * yAxis);
    }
}
