using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float yAxis;
    float enginePower = 1f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Banking controls, turning turning left and right on Z axis
        rb.AddTorque(Input.GetAxis("Horizontal") * transform.forward * -0.5f);

        //Pitch controls, turning the nose up and down
        rb.AddTorque(Input.GetAxis("Vertical") * transform.right * yAxis);

        rb.velocity *= 0.5f;
    }
}
