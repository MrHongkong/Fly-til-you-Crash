using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExhaustEngineController : MonoBehaviour
{
    ParticleSystem ps;
    public float LerpT;
    public float nextEnginePower;

    public float power;

    // Start is called before the first frame update
    void Start() {
        ps = GetComponent<ParticleSystem>();
        nextEnginePower = 0;
    }

    public void SetNextEnginePower(float power) {
        this.nextEnginePower = power;
    }
    
    public void UpdatePower() {
        power = Mathf.Lerp(power, nextEnginePower, LerpT);
        var col = ps.colorOverLifetime;
        
        ParticleSystem.MinMaxGradient m = col.color;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(
                    new Color(
                        0.5599999f, 
                        0f + 0.5f * power, 
                        1f), 
                    0f),
                new GradientColorKey(
                    new Color(
                        0f + 0.5f * power, 
                        0.8005764f + 0.19f * power, 
                        0.8867924f + 0.1f * power), 
                    0.7441214f)}, 
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.1294118f + (1f - 0.1294118f) * power, 0f),
                new GradientAlphaKey(0f, 0.402945f),
                new GradientAlphaKey(0.4666667f, 0.6382391f),
                new GradientAlphaKey(0f, 1f)});
        
        col.color = grad; 
        nextEnginePower = 0f;
    }

    public bool IsActive()
    {
        return power > 0.1f;
    }
}
