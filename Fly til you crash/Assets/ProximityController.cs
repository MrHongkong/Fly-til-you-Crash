using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityController : MonoBehaviour
{
    Material lightingMaterial;
    public LayerMask wallLayer;
    public bool drawGizmo;
    public float radius;

    public float counter = float.MaxValue;
    public AnimationCurve intensityCurve;
    public float intensityModifier;
    public AnimationCurve opacityCurve;

    public List<ParticleSystem> lightTails;

    private void Start(){
        
        Material[] materials = GetComponent<Renderer>().materials;
        lightingMaterial = materials[3];
        foreach (ParticleSystem ps in lightTails)
            ps.GetComponent<Renderer>().material = lightingMaterial;
        GetComponent<Renderer>().materials = materials;
    }

    float GetAnimationWidth(AnimationCurve ac){
        float[] keyTimes = new float[ac.keys.Length];

        for (int i = 0; i < ac.length; i++)
            keyTimes[i] = ac.keys[i].time;

        //Assumes the other value is set to 0
        return Mathf.Max(keyTimes);
    }

    bool IsActive(){
        return counter < GetAnimationWidth(intensityCurve);
    }

    // Update is called once per frame
    void Update(){
        if (PromiximityTrigger() && !IsActive()){
            AudioManager.instance.Play("ProximityWarning");
            counter = 0f;
        }

        if (IsActive())
            counter += Time.deltaTime * 1 / Time.timeScale;

        foreach (ParticleSystem ps in lightTails)
            if (IsActive() && intensityCurve.Evaluate(counter) > 0.15f)
                ps.Play();
            else
                ps.Stop();

        lightingMaterial.SetColor("_EmissionColor", new Color(191f, 0f, 0f) * intensityCurve.Evaluate(counter) * intensityModifier);
    }

    bool PromiximityTrigger(){
        return Physics.OverlapSphere(transform.position, radius, wallLayer).Length > 0;
    }

    void OnDrawGizmos(){
        if (drawGizmo)
            Gizmos.DrawSphere(transform.position, radius);
    }
}
