using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script made by Adam */
public class CameraController : MonoBehaviour
{
    public Transform player;
    public float fov;
    
    public float distanceInfrontOfPlayer;
    public float distanceBehindPlayer;
    public float distanceAbovePlayer;

    [Range(0.01f, 3f)]
    public float cameraLerp;
    [Range(0.01f, 3f)]
    public float cameraLerpSlowmo;
    [Range(0.01f, 3f)]
    public float cameraLerpFastmo;
    
    Vector3 attention;
    
    void Start() {
        attention = player.position + distanceInfrontOfPlayer * player.forward;
    }

    void LateUpdate() {
        Vector3 cameraLocation = player.position + -1f * distanceBehindPlayer * player.forward + distanceAbovePlayer * player.up;
        Vector3 rayDirection = distanceBehindPlayer * -player.forward + distanceAbovePlayer * player.up;

        Vector3 BackwardsToCamera = Vector3.Project(rayDirection, -player.forward);
        Vector3 UpwardsToCamera = rayDirection - BackwardsToCamera;

        Debug.DrawLine(player.position, player.position + BackwardsToCamera);
        Debug.DrawLine(player.position + BackwardsToCamera, player.position + BackwardsToCamera + UpwardsToCamera);

        RaycastHit raycastHit = new RaycastHit();
        bool rayHit = Physics.Raycast(player.position, rayDirection, out raycastHit, rayDirection.magnitude);
        
        if(rayHit) {
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
        attention = Vector3.Lerp(attention, (player.position + distanceInfrontOfPlayer * player.forward), t);
        transform.LookAt(attention, player.up);
    }
}
