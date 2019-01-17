using UnityEngine;

public class Acceleration : MonoBehaviour
{

    public float maxSpeed = 1000f;
    public float timeZeroToMax = 50f;
    float accelRatePerSec;
    float forwardVelocity;

    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        accelRatePerSec = maxSpeed / timeZeroToMax;
        forwardVelocity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        forwardVelocity += accelRatePerSec * Time.deltaTime;
        forwardVelocity = Mathf.Min (forwardVelocity, maxSpeed);
    }

    void LateUpdate()
    {
        rb.velocity = transform.forward * forwardVelocity;   
    }
}
