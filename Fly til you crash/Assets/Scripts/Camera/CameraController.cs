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

    [Range(0.01f, 0.99f)]
    public float cameraBias;
    [Range(0.01f, 0.99f)]
    public float cameraBiasSlowmo;
    [Range(0.01f, 0.99f)]
    public float cameraBiasFastmo;

    [Range(0.01f, 0.99f)]
    public float cameraAttentionBias;
    [Range(0.01f, 0.99f)]
    public float cameraAttentionBiasSlowmo;
    [Range(0.01f, 0.99f)]
    public float cameraAttentionBiasFastmo;
    Vector3 attention;
    
    void Start()
    {
        attention = player.position + distanceInfrontOfPlayer * player.forward;
    }

    void LateUpdate()
    {
        Vector3 cameraLocation = player.position + -1f * distanceBehindPlayer * player.forward + distanceAbovePlayer * player.up;

        Vector3 rayDirection = cameraLocation - player.position;
        
        RaycastHit raycastHit = new RaycastHit();
        float reduction = 2f;
        while(Physics.Raycast(player.position, rayDirection, out raycastHit, rayDirection.magnitude)) {
            reduction -= 0.1f;
            if ((reduction - 1f) > 0.4f)
                cameraLocation = player.position + -1f * distanceBehindPlayer * reduction * player.forward + distanceAbovePlayer * (reduction - 1f) * player.up;
            else
                cameraLocation = player.position + -1f * (distanceBehindPlayer * (reduction / 2f) * player.forward + (distanceAbovePlayer * 0.2f * (reduction - 1f) * player.up));
            rayDirection = cameraLocation - player.position;
        }

        if (PlayerController.playerController.IsSlowMotion())
        { 
            Camera.main.transform.position = (Camera.main.transform.position * cameraBiasSlowmo) + (cameraLocation * (1f - cameraBiasSlowmo));
            attention = (attention * cameraAttentionBiasSlowmo) + ((player.position + distanceInfrontOfPlayer * player.forward) * (1f - cameraAttentionBiasSlowmo));
        }
        else if(PlayerController.playerController.IsFastMotion())
        {
            Camera.main.transform.position = (Camera.main.transform.position * cameraBiasFastmo) + (cameraLocation * (1f - cameraBiasFastmo));
            attention = (attention * cameraAttentionBiasFastmo) + ((player.position + distanceInfrontOfPlayer * player.forward) * (1f - cameraAttentionBiasFastmo));
        }
        else
        {
            Camera.main.transform.position = (Camera.main.transform.position * cameraBias) + (cameraLocation * (1f - cameraBias));
            attention = (attention * cameraAttentionBias) + ((player.position + distanceInfrontOfPlayer * player.forward) * (1f - cameraAttentionBias));
        }
        Camera.main.transform.LookAt(attention, player.up);
    }
}
