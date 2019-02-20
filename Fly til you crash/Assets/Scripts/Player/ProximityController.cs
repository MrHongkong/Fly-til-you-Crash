using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityController : MonoBehaviour
{
    public Material lightingMaterial;
    public Transform promixityLight;

    public LayerMask wallLayer;
    public bool drawGizmo;
    public float radius;

    public float counter = float.MaxValue;
    public AnimationCurve intensityCurve;
    public float intensityModifier;
    public AnimationCurve opacityCurve;
    

    private void Start(){
        lightingMaterial = new Material(lightingMaterial);
        promixityLight.GetComponent<Renderer>().material = lightingMaterial;
    }

    bool IsActive(){
        return counter < ExhaustEngineController.GetAnimationWidth(intensityCurve);
    }

    // Update is called once per frame
    void Update(){
        if (PromiximityTrigger() && !IsActive()){
            AudioManager.instance.Play("ProximityWarning");
            counter = 0f;
        }

        if (IsActive())
            counter += Time.deltaTime * 1 / Time.timeScale;
        
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
