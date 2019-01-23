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
        rb.AddTorque(Input.GetAxis("Horizontal") * transform.forward * -0.5f * bankingTorqueAmp);

        //Pitch controls, turning the nose up and down
        rb.AddTorque(Input.GetAxis("Vertical") * transform.right * yAxis * pitchingTorqueAmp);

        rb.velocity *= 0.5f;

        if (Mathf.Abs(Input.GetAxisRaw("Yaw")) > 0.1f)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.y += Input.GetAxisRaw("Yaw") * 150f * Time.deltaTime;
            transform.eulerAngles = eulerAngles;
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
