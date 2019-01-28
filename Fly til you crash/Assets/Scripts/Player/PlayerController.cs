using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float yAxis;
    public float bankingTorqueAmp;
    public float pitchingTorqueAmp;

    public float dragOnHold;
    public float dragOffHold;

    public ParticleSystem exhaustFire;

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
        if (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude < 0.2f)
            rb.angularDrag = dragOnHold;
        else
            rb.angularDrag = dragOffHold;

        //Banking controls, turning turning left and right on Z axis
        rb.AddTorque(Input.GetAxisRaw("Yaw") * transform.forward * -0.5f * bankingTorqueAmp);

        //Pitch controls, turning the nose up and down
        rb.AddTorque(Input.GetAxis("Vertical") * transform.right * yAxis * pitchingTorqueAmp);

        rb.velocity *= 0.6f;

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f)
        {
            rb.AddTorque(transform.up * Input.GetAxisRaw("Horizontal") * 75f * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        var emission = exhaustFire.emission;
        var rate = emission.rate;

        rate.constantMax = rb.velocity.magnitude * 0.75f;
        emission.rate = rate;
    }

}
