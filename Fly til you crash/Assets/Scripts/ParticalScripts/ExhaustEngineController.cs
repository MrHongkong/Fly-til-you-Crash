using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExhaustEngineController : MonoBehaviour
{
    /*
    ParticleSystem ps;
    public float LerpT;
    public float nextEnginePower;

    public float power;

    public Color[] color;
    public float[] colorLocation;

    public float[] alpha;
    public float[] alphaLocation;
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

        GradientColorKey[] gradientColorKeys = new GradientColorKey[color.Length];
        GradientAlphaKey[] gradientAlphaKeys = new GradientAlphaKey[alpha.Length];

        //grad.SetKeys(
        //    new GradientColorKey[] {
        //        new GradientColorKey(
        //            new Color(
        //                0.5599999f, 
        //                0f + 0.5f * power, 
        //                1f), 
        //            0f),
        //        new GradientColorKey(
        //            new Color(
        //                0f + 0.5f * power, 
        //                0.8005764f + 0.19f * power, 
        //                0.8867924f + 0.1f * power), 
        //            0.7441214f)}, 
        //    new GradientAlphaKey[] {
        //        new GradientAlphaKey(0.1294118f + (1f - 0.1294118f) * power, 0f),
        //        new GradientAlphaKey(0f, 0.402945f),
        //        new GradientAlphaKey(0.4666667f, 0.6382391f),
        //        new GradientAlphaKey(0f, 1f)});

        grad.SetKeys(gradientColorKeys, gradientAlphaKeys);

        col.color = grad; 
        nextEnginePower = 0f;
    }

    public bool IsActive()
    {
        return power > 0.1f;
    }*/

    ParticleSystem ps;

    public float startEmission;
    public float startLifetime;
    public float startSize;

    public AnimationCurve emissionBoost;
    public AnimationCurve lifetimeBoost;
    public AnimationCurve sizeBoost;

    public float counter = float.MaxValue;
    
    public static float GetAnimationWidth(AnimationCurve ac){
        float[] keyTimes = new float[ac.keys.Length];

        for (int i = 0; i < ac.length; i++)
            keyTimes[i] = ac.keys[i].time;

        //Assumes the other value is set to 0
        return Mathf.Max(keyTimes);
    }

    void Start(){
        ps = GetComponent<ParticleSystem>();
    }

    bool IsActive(){
        return counter < Mathf.Max(new float[] { GetAnimationWidth(lifetimeBoost), GetAnimationWidth(sizeBoost) });
    }

    void Update(){
        //Debug.Log(PlayerController.playerController.IsFastMotion());
        if (IsActive()){
            float lifetimeCalc = lifetimeBoost.Evaluate(counter + Time.deltaTime);
            float sizeCalc = sizeBoost.Evaluate(counter + Time.deltaTime);
            float emissionCalc = emissionBoost.Evaluate(counter + Time.deltaTime);

            //If it's at the peak and the player is boosting, it won't progress further.
            if ((lifetimeBoost.Evaluate(counter) > lifetimeCalc && !PlayerController.playerController.IsFastMotion()) ||
                (lifetimeBoost.Evaluate(counter) < lifetimeCalc && PlayerController.playerController.IsFastMotion()))
                counter += Time.deltaTime;

            ps.startLifetime = startLifetime * lifetimeCalc;
            ps.startSize = startSize * sizeCalc;
            ps.emissionRate = startEmission * emissionCalc;
        }
        else{
            ps.startLifetime = startLifetime;
            ps.startSize = startSize;
            ps.emissionRate = startEmission;
        }
    }

    public void Boost(){
        counter = 0f;
    }
}
