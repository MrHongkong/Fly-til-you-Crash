using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //Add force from jet engine , set enginePower to 15000
        rb.AddForce(transform.forward * enginePower);
        
        //Banking controls, turning turning left and right on Z axis
        rb.AddTorque(Input.GetAxis("Horizontal") * transform.forward);

        //Pitch controls, turning the nose up and down
        rb.AddTorque(Input.GetAxis("Vertical") * transform.right * yAxis);
        
        //Set drag and angular drag according relative to speed
        //rb.drag = 0.001f * rb.velocity.magnitude;
        //rb.angularDrag = 0.01f * rb.velocity.magnitude;

        rb.velocity *= 0.9f;
    }
}
