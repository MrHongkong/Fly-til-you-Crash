using UnityEngine;

public class Acceleration : MonoBehaviour
{
    //Made by Philip Åkerblom GP18 Yrgo
    public float forwardVelocity;
    public float speedIncrease;
    public float secondsToNextSpeedIncrease;
    float fov;
    float nextSpeedIncrease;
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        nextSpeedIncrease = 0;
        fov = 60;
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
            if(fov < 130)
            {
                fov = fov + 10;    
                Camera.main.fieldOfView = fov;
                Debug.Log(fov);
            }
        }
    }
}
