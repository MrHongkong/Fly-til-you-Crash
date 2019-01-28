using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script made by Adam */
public class CameraController : MonoBehaviour
{
    public Transform player;
    public float fov;
    
    [HideInInspector]
    public float distanceInfrontOfPlayer = 5;
    [HideInInspector]
    public float distanceBehindPlayer = 10;
    [HideInInspector]
    public float distanceAbovePlayer = 3;

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
