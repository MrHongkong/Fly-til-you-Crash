using UnityEngine;

public class Acceleration : MonoBehaviour
{
    //Made by Philip Åkerblom GP18 Yrgo
    public float forwardVelocity;
    public float speedIncrease;
    public float secondsToNextSpeedIncrease;
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
    }

    void SpeedIncrease()
    {
        if (Time.time > nextSpeedIncrease)
        {
            forwardVelocity += speedIncrease;
            nextSpeedIncrease = Time.time + secondsToNextSpeedIncrease;
        }
    }
}
