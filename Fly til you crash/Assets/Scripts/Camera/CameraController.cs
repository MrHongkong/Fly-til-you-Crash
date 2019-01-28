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
    public float cameraAttentionBias;
    Vector3 attention;
    
    void Start()
    {
        attention = player.position + distanceInfrontOfPlayer * player.forward;
    }

    void LateUpdate()
    {
        Vector3 CameraLocation = player.position + -1f * distanceBehindPlayer * player.forward + distanceAbovePlayer * player.up;
        Camera.main.transform.position = (Camera.main.transform.position * cameraBias) + (CameraLocation * (1f - cameraBias));

        attention = (attention * cameraAttentionBias) + ((player.position + distanceInfrontOfPlayer * player.forward) * (1f - cameraAttentionBias));
        Camera.main.transform.LookAt(attention, player.up);
    }
}
