using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float yAxis;
    public float bankingTorqueAmp;
    public float pitchingTorqueAmp;

    [SerializeField][Range(1f, 2f)] private float controlPrecentage;

    public float dragOnHold;
    public float dragOffHold;

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

    public float slowMotionLeftCounter;
    float slowMotionMaxCounter;
    public AnimationCurve slowMotionCurve;
    public float slowMotionCounter = float.MaxValue;
    
    public AnimationCurve fastMotionCurve;
    public float fastMotionCounter = float.MaxValue;

    public AnimationCurve fovCurve;

    public static PlayerController playerController;
    
    // Start is called before the first frame update
    void Start(){
        slowMotionMaxCounter = slowMotionLeftCounter;
        AudioManager.instance.Play("CarSound");
        rb = movable.GetComponent<Rigidbody>();
        angles = movable.localEulerAngles;

        if (playerController == null)
            playerController = this;
        else
            Destroy(this);

        yAxis = MenuSettings.getInvertedControls() ? 1f : -1f;
    }

    public float GetPercentageBoost(){
        return slowMotionLeftCounter / slowMotionMaxCounter;
    }

    // Update is called once per frame
    void Update(){
        if (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude < 0.2f){rb.angularDrag = dragOnHold;}
        else{rb.angularDrag = dragOffHold;}
       
        if((!IsFastMotion() && Input.GetButton("Fastmotion")) || (!IsSlowMotion() && Input.GetButton("Slowmotion") && slowMotionLeftCounter > 0f)){
            if(!IsFastMotion() && Input.GetButton("Fastmotion")){
                fastMotionCounter = 0f;
                foreach (ExhaustEngineController eec in exhaustEngines)
                    eec.Boost();
            }
            else if (!IsSlowMotion() && Input.GetButton("Slowmotion") && slowMotionLeftCounter > 0f){
                slowMotionCounter = 0f;
            }
        }

        //Logger.Log(Time.timeScale);

        //Logger.Log(IsSlowMotion());

        //Logger.Log(Time.timeScale);

        //Debug.Log(slowMotionLeftCounter);
        //Debug.Log(Time.timeScale);

        if (IsSlowMotion()){
            float slowmotionCalc = slowMotionCurve.Evaluate(slowMotionCounter + Time.deltaTime);
            if ((slowMotionCurve.Evaluate(slowMotionCounter) < slowmotionCalc && !Input.GetButton("Slowmotion")) ||
                (slowMotionCurve.Evaluate(slowMotionCounter) >= slowmotionCalc && Input.GetButton("Slowmotion")) ||
                !Input.GetButton("Slowmotion") ||
                slowMotionLeftCounter < 0f)
                slowMotionCounter += Time.deltaTime;

            if (slowMotionLeftCounter > 0f)
                slowMotionLeftCounter -= Time.deltaTime;
            Time.timeScale = slowMotionCurve.Evaluate(slowMotionCounter);
        }
        else if(IsFastMotion()){
            float fastMotionCalc = fastMotionCurve.Evaluate(fastMotionCounter + Time.deltaTime);
            if ((fastMotionCurve.Evaluate(fastMotionCounter) >= fastMotionCalc && !Input.GetButton("Fastmotion")) ||
                (fastMotionCurve.Evaluate(fastMotionCounter) < fastMotionCalc && Input.GetButton("Fastmotion")) ||
                !Input.GetButton("Fastmotion"))
                fastMotionCounter += Time.deltaTime * 1 / Time.timeScale;
            Time.timeScale = fastMotionCurve.Evaluate(fastMotionCounter);
        }
        else if(!Input.GetButton("Slowmotion") && !Input.GetButton("Fastmotion")){
            if (slowMotionLeftCounter < slowMotionMaxCounter)
                slowMotionLeftCounter += Time.deltaTime;
            Time.timeScale = 1f;
        }
        
        /*
        if (fastmotion ^ Input.GetButton("Fastmotion")){
            if (!alreadyPlayed){
                //AudioManager.instance.Play("BoostSound");
                alreadyPlayed = true;
            }
            else{
                alreadyPlayed = false;
            }
            fastmotion = Input.GetButton("Fastmotion");

            if (fastmotion)
                foreach (ExhaustEngineController eec in exhaustEngines)
                    eec.Boost();
        }

        if (slowmotion ^ Input.GetButton("Slowmotion"))
        {
            AudioManager.instance.Play("SlowMotionSound");
            slowmotion = Input.GetButton("Slowmotion");
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
        }*/

        //Pitch controls, turning the nose up and down

        rb.velocity *= 0.6f;
    }

    void FixedUpdate()
    {
        float t_scale = Mathf.Max(new float[] { 1f, Time.timeScale });
        turnAcceleration.x = Mathf.Lerp(turnAcceleration.x, Input.GetAxisRaw("Vertical") * 50f * controlPrecentage, 2f);
        turnAcceleration.y = Mathf.Lerp(turnAcceleration.y, Input.GetAxisRaw("Horizontal") * 50f * controlPrecentage, 2f);
        turnAcceleration.z = Mathf.Lerp(turnAcceleration.z, Input.GetAxisRaw("Yaw") * 100f * controlPrecentage, 2f);

        movable.localRotation *= Quaternion.Euler(turnAcceleration.x * Time.deltaTime * (1f / t_scale) * yAxis, 0f, 0f);
        movable.localRotation *= Quaternion.Euler(0f, turnAcceleration.y * Time.deltaTime * (1f / t_scale), 0f);
        movable.localRotation *= Quaternion.Euler(0f, 0f, -turnAcceleration.z * Time.deltaTime * (1f / t_scale));

        float emissionRateHoverEnginesRight;

        if (turnAcceleration.z < 0)
        {
            foreach (HoverEngineController hec in rightHoverEngines)
            {
                hec.SetNextEnginePower(-turnAcceleration.z / 100);
            }
        }

        else if (turnAcceleration.z > 0)
        {
            foreach (HoverEngineController hec in leftHoverEngines)
            {
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
    }

    public bool IsSlowMotion() { return slowMotionCounter < ExhaustEngineController.GetAnimationWidth(slowMotionCurve); }
    public bool IsFastMotion() { return fastMotionCounter < ExhaustEngineController.GetAnimationWidth(fastMotionCurve); }

}
