using UnityEngine;

public class Acceleration : MonoBehaviour
{
    //Made by Philip Åkerblom GP18 Yrgo
    public float forwardVelocity;
    public float secondsToNextSpeedIncrease = 60;
    float nextSpeedIncrease;
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        nextSpeedIncrease = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SpeedIncrease();
        rb.velocity = transform.forward * forwardVelocity;
        Debug.Log(rb.velocity);
    }

    void SpeedIncrease()
    {
        if (Time.time > nextSpeedIncrease)
        {
            forwardVelocity += 10;
            nextSpeedIncrease = Time.time + secondsToNextSpeedIncrease;
            Debug.Log(nextSpeedIncrease);
        }
    }
}
