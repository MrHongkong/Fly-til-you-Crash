using UnityEngine;

public class Acceleration : MonoBehaviour
{
    //Made by Philip Åkerblom GP18 Yrgo

    private float maxSpeed = 1000f;
    private float timeZeroToMax = 2000f;
    private float accelRatePerSec;
    private float forwardVelocity;

    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        accelRatePerSec = maxSpeed / timeZeroToMax;
        forwardVelocity = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        forwardVelocity += accelRatePerSec * Time.deltaTime;
        forwardVelocity = Mathf.Min (forwardVelocity, maxSpeed);
        Debug.Log("Acceleration: " + forwardVelocity);
    }

    void LateUpdate()
    {
        rb.velocity = transform.forward * forwardVelocity;   
    }
}
