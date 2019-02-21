using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

/* Script made by Adam */
public class CameraController : MonoBehaviour
{
    public Transform player;
    public AnimationCurve fovCurve;
       
    public AnimationCurve distanceInfrontOfPlayer;
    public AnimationCurve distanceBehindPlayer;
    public AnimationCurve distanceAbovePlayer;

    public bool DrawGizmos;

    [Range(0.01f, 3f)]
    public float cameraLerp;
    [Range(0.01f, 3f)]
    public float cameraLerpSlowmo;
    [Range(0.01f, 3f)]
    public float cameraLerpFastmo;
    
    Vector3 attention;
    
    void Start() {
        attention = player.position + distanceInfrontOfPlayer.Evaluate(0f) * player.forward;
        CameraShaker.Instance.StartShake(2f, 10f, 700f);
    }

    void Update(){
        Camera.main.fieldOfView = 80f + fovCurve.Evaluate(Time.timeSinceLevelLoad);
    }

    void LateUpdate() {
        Vector3 cameraLocation = player.position + distanceBehindPlayer.Evaluate(Time.timeSinceLevelLoad) * -player.forward + 
            distanceAbovePlayer.Evaluate(Time.timeSinceLevelLoad) * player.up;
        Vector3 rayDirection = distanceBehindPlayer.Evaluate(Time.timeSinceLevelLoad) * -player.forward + 
            distanceAbovePlayer.Evaluate(Time.timeSinceLevelLoad) * player.up;

        Vector3 BackwardsToCamera = Vector3.Project(rayDirection, -player.forward);
        Vector3 UpwardsToCamera = rayDirection - BackwardsToCamera;
        
        RaycastHit raycastHit = new RaycastHit();
        bool rayHit = Physics.Raycast(player.position, rayDirection, out raycastHit, rayDirection.magnitude);

         if (rayHit) {
            Vector3 RayBackwardsToCamera = Vector3.Project(raycastHit.point - player.position, -player.forward);
            Vector3 RayUpwardsToCamera = (raycastHit.point - player.position) - RayBackwardsToCamera;

            if (RayUpwardsToCamera.sqrMagnitude < UpwardsToCamera.sqrMagnitude * 0.7)
                {cameraLocation = player.position + BackwardsToCamera - UpwardsToCamera * 0.2f * (1f - (RayUpwardsToCamera.sqrMagnitude / UpwardsToCamera.sqrMagnitude));}
            else
                {cameraLocation = player.position + BackwardsToCamera + RayUpwardsToCamera * 0.9f;}
        }

        float t;
        if (PlayerController.playerController.IsSlowMotion()) {t = cameraLerpSlowmo; }
        else if(PlayerController.playerController.IsFastMotion()) {t = cameraLerpFastmo; }
        else {t = cameraLerp; }


        transform.position = Vector3.Lerp(transform.position, cameraLocation, t);
        attention = Vector3.Lerp(attention, (player.position + distanceInfrontOfPlayer.Evaluate(Time.timeSinceLevelLoad) * player.forward), t);
        transform.LookAt(attention, player.up);
    }

    private void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.DrawLine(
                player.position,
                player.position + -player.forward * distanceBehindPlayer.Evaluate(0f));
            Gizmos.DrawLine(
                player.position + -player.forward * distanceBehindPlayer.Evaluate(0f),
                player.position + -player.forward * distanceBehindPlayer.Evaluate(0f) + player.up * distanceAbovePlayer.Evaluate(0f));

            Vector3 cameraLocation = player.position + distanceBehindPlayer.Evaluate(Time.timeSinceLevelLoad) * -player.forward +
            distanceAbovePlayer.Evaluate(Time.timeSinceLevelLoad) * player.up;
            Vector3 rayDirection = distanceBehindPlayer.Evaluate(Time.timeSinceLevelLoad) * -player.forward +
                distanceAbovePlayer.Evaluate(Time.timeSinceLevelLoad) * player.up;

            RaycastHit raycastHit = new RaycastHit();
            bool rayHit = Physics.Raycast(player.position, rayDirection, out raycastHit, rayDirection.magnitude);

            if (rayHit)
            {
                Vector3 RayBackwardsToCamera = Vector3.Project(raycastHit.point - player.position, -player.forward);
                Vector3 RayUpwardsToCamera = (raycastHit.point - player.position) - RayBackwardsToCamera;

                Gizmos.DrawLine(player.position, player.position + RayBackwardsToCamera);
                Gizmos.DrawLine(
                    player.position + RayBackwardsToCamera,
                    player.position + RayBackwardsToCamera + RayUpwardsToCamera);

            }
        }
    }
}
