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

    public List<ParticleSystem> exhaustFire;

    public List<HoverEngineController> leftHoverEngines;
    public List<HoverEngineController> rightHoverEngines;
    public List<HoverEngineController> frontHoverEngines;
    public List<HoverEngineController> backHoverEngines;

    public List<ExhaustEngineController> exhaustEngines;

    float enginePower = 1f;
    public Transform movable;
    Rigidbody rb;
    
    Vector3 turnAcceleration = Vector3.zero;
    Vector3 angles;
    float shipAngle = 0f;

    bool exhaust = true;
    bool alreadyPlayed = false;

    bool slowmotion = false;
    float slowmotionTimer = 2f;
    public float slowTimeScale;

    bool fastmotion = false;
    public float fastTimeScale;
    
    public static PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("CarSound");
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
        {           
            rb.angularDrag = dragOnHold;
        }
        else
        {
            rb.angularDrag = dragOffHold;
        }
       
        if (fastmotion ^ Input.GetButton("TimeWarp"))
        {
            if (!alreadyPlayed)
            {
                //FindObjectOfType<AudioManager>().Play("BoostSound");
                alreadyPlayed = true;
            }
            else
            {
                alreadyPlayed = false;
            }
            fastmotion = Input.GetButton("TimeWarp");
        }

        if (slowmotion ^ Input.GetButton("TimeStop"))
        {
            FindObjectOfType<AudioManager>().Play("SlowMotionSound");
            slowmotion = Input.GetButton("TimeStop");
        }


        if ((slowmotion && slowmotionTimer > 0f) || fastmotion)
        {
            if (slowmotion && slowmotionTimer > 0f)
            {
                slowmotionTimer -= Time.deltaTime * 10f * slowTimeScale;              
                Time.timeScale = slowTimeScale;
            }
            else
            {
                Time.timeScale = fastTimeScale;
            }
        }
        else
        {
            if (slowmotionTimer < 2f)
                slowmotionTimer += Time.deltaTime * slowTimeScale;
            Time.timeScale = 1f;
        }

        //Pitch controls, turning the nose up and down

        rb.velocity *= 0.6f;
    }
    
    void FixedUpdate()
    {
        turnAcceleration.x = Mathf.Lerp(turnAcceleration.x, Input.GetAxisRaw("Vertical") * 50f, 2f);
        turnAcceleration.y = Mathf.Lerp(turnAcceleration.y, Input.GetAxisRaw("Horizontal") * 50f, 2f);
        turnAcceleration.z = Mathf.Lerp(turnAcceleration.z, Input.GetAxisRaw("Yaw") * 100f, 2f);

        movable.localRotation *= Quaternion.Euler(turnAcceleration.x * Time.deltaTime * (1f / Time.timeScale), 0f, 0f);
        movable.localRotation *= Quaternion.Euler(0f, turnAcceleration.y * Time.deltaTime * (1f / Time.timeScale), 0f);
        movable.localRotation *= Quaternion.Euler(0f, 0f, -turnAcceleration.z * Time.deltaTime * (1f / Time.timeScale));

        float emissionRateHoverEnginesRight;

        if(turnAcceleration.z < 0) {
            foreach(HoverEngineController hec in rightHoverEngines) {
                hec.SetNextEnginePower(-turnAcceleration.z / 100);
            }
        }

        else if(turnAcceleration.z > 0) {
            foreach (HoverEngineController hec in leftHoverEngines) {
                hec.SetNextEnginePower(turnAcceleration.z / 100);
            }
        }

        if (turnAcceleration.x < 0)
        {
            foreach (HoverEngineController hec in frontHoverEngines)
            {
                hec.SetNextEnginePower(-turnAcceleration.x / 50);
            }
        }

        else if (turnAcceleration.x > 0)
        {
            foreach (HoverEngineController hec in backHoverEngines)
            {
                hec.SetNextEnginePower(turnAcceleration.x / 50);
            }
        }

        foreach (HoverEngineController hec in rightHoverEngines)
            hec.UpdatePower();
        foreach (HoverEngineController hec in leftHoverEngines)
            hec.UpdatePower();
        //angles.x += turnAcceleration.x * Time.deltaTime;
        //angles.y += turnAcceleration.y * Time.deltaTime;
        //movable.localEulerAngles = angles;

        shipAngle = Mathf.Lerp(shipAngle, Input.GetAxisRaw("Horizontal") * 45f, 0.02f);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -shipAngle);
        
        float emissionRate;
        if (exhaust)
        {
            if (IsSlowMotion())
                emissionRate = rb.velocity.magnitude * 10f;
            else if (IsFastMotion())
            {
                foreach (ExhaustEngineController eec in exhaustEngines)
                {
                    eec.SetNextEnginePower(1f);
                }
                emissionRate = 100f;
            }
            else
            {
                foreach (ExhaustEngineController eec in exhaustEngines)
                {
                    eec.power *= 0.9f;
                }
                emissionRate = 0f;
            }
        }
        else
        {
            emissionRate = 0f;
            foreach (ExhaustEngineController eec in exhaustEngines)
            {
                eec.power *= 0.9f;
            }
        }
        
        foreach (ExhaustEngineController eec in exhaustEngines)
        {
            eec.UpdatePower();
        }

        foreach (ParticleSystem ps in exhaustFire) {
            var emission = ps.emission;
            var rate = emission.rateOverTime;
            rate.constantMax = emissionRate;
            emission.rateOverTime = rate;
        }
    }

    public void EnableExhaust() { exhaust = true; }
    public void DisableExhaust() { exhaust = false; }

    public bool IsSlowMotion() { return slowmotion && slowmotionTimer > 0f; }
    public bool IsFastMotion() { return fastmotion; }

}
