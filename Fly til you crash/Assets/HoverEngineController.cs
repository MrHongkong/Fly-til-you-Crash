using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class HoverEngineController : MonoBehaviour
{
    ParticleSystem ps;
    public float LowPoweredState;
    public float HighPoweredState;

    float TargetStartLifetime;
    float CurrentStartLifetime;

    public float bias;
    public float nextEnginePower;

    float power;

    // Start is called before the first frame update
    void Start() {
        ps = GetComponent<ParticleSystem>();
        nextEnginePower = 0;
    }

    public void SetNextEnginePower(float power) {
        if(power > nextEnginePower) {
            nextEnginePower = power;
        }
    }
    
    public void UpdatePower() {
        power = (power * bias) + nextEnginePower * (1f - bias);
        ps.startLifetime = LowPoweredState + (HighPoweredState - LowPoweredState) * power;

        nextEnginePower = 0f;

        //Debug.Log("power = " + power);
    }

    public bool IsActive()
    {
        return power > 0.1f;
    }
}
