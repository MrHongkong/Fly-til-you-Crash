using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float yAxis;
    public float bankingTorqueAmp;
    public float pitchingTorqueAmp;

    public float dragOnHold;
    public float dragOffHold;

    public ParticleSystem exhaustFire;

    float enginePower = 1f;
    public Transform movable;
    Rigidbody rb;

    Vector3 turnAcceleration = Vector3.zero;
    Vector3 angles;
    float shipAngle = 0f;

    bool exhaust = true;

    bool slowmotion = false;
    float slowmotionTimer = 2f;
    public float slowTimeScale;

    bool fastmotion = false;
    float fastmotionTimer = 40f;
    public float fastTimeScale;

    public static PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        rb = movable.GetComponent<Rigidbody>();
        angles = movable.localEulerAngles;

        if (playerController == null)
            playerController = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude < 0.2f)
            rb.angularDrag = dragOnHold;
        else
            rb.angularDrag = dragOffHold;

        if (slowmotion ^ Input.GetKey(KeyCode.Joystick1Button0))
        {
            slowmotion = Input.GetKey(KeyCode.Joystick1Button0);
        }

        if (fastmotion ^ Input.GetKey(KeyCode.Joystick1Button1))
        {
            fastmotion = Input.GetKey(KeyCode.Joystick1Button1);
        }

        if ((slowmotion && slowmotionTimer > 0f) || (fastmotion && fastmotionTimer > 0f))
        {
            if (slowmotion && slowmotionTimer > 0f)
            {
                slowmotionTimer -= Time.deltaTime * 10f * slowTimeScale;
                Time.timeScale = slowTimeScale;
            }
            else
            {
                fastmotionTimer -= Time.deltaTime;
                Time.timeScale = fastTimeScale;
            }
        }
        else
        {
            if (slowmotionTimer < 2f)
                slowmotionTimer += Time.deltaTime * slowTimeScale;
            if (fastmotionTimer < 40f)
                fastmotionTimer += Time.deltaTime;
            Time.timeScale = 1f;
        }

        //Pitch controls, turning the nose up and down

        rb.velocity *= 0.6f;
    }
    
    void FixedUpdate()
    {
        turnAcceleration.x = Mathf.Lerp(turnAcceleration.x, Input.GetAxisRaw("Vertical") * 50f, 3f);
        turnAcceleration.y = Mathf.Lerp(turnAcceleration.y, Input.GetAxisRaw("Horizontal") * 50f, 3f);
        turnAcceleration.z = Mathf.Lerp(turnAcceleration.z, Input.GetAxisRaw("Yaw") * 100f, 3f);

        movable.localRotation *= Quaternion.Euler(turnAcceleration.x * Time.deltaTime, 0f, 0f);
        movable.localRotation *= Quaternion.Euler(0f, turnAcceleration.y * Time.deltaTime, 0f);
        movable.localRotation *= Quaternion.Euler(0f, 0f, -turnAcceleration.z * Time.deltaTime);

        
       //angles.x += turnAcceleration.x * Time.deltaTime;
       //angles.y += turnAcceleration.y * Time.deltaTime;
       //movable.localEulerAngles = angles;

       shipAngle = Mathf.Lerp(shipAngle, Input.GetAxisRaw("Horizontal") * 45f, 0.02f);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -shipAngle);

        var emission = exhaustFire.emission;
        var rate = emission.rateOverTime;

        if (exhaust)
        {
            if (IsSlowMotion())
                rate.constantMax = rb.velocity.magnitude * 0;
            else if (IsFastMotion())
                rate.constantMax = rb.velocity.magnitude * 50f;
            else
                rate.constantMax = rb.velocity.magnitude * 0f;
        }
        else
            rate.constantMax = 0f;
        emission.rateOverTime = rate;
    }

    public void EnableExhaust() { exhaust = true; }
    public void DisableExhaust() { exhaust = false; }

    public bool IsSlowMotion() { return slowmotion && slowmotionTimer > 0f; }
    public bool IsFastMotion() { return fastmotion && fastmotionTimer > 0f; }

}
