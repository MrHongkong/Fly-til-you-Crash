using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float fov;

    [Range(0.01f, 0.99f)]
    public float cameraBias;

    [Range(0.01f, 0.99f)]
    public float cameraAttentionBias;
    Vector3 attention;

    public void SetFOV(float fov)
    {
        Camera.main.fieldOfView = fov;
        this.fov = fov;
    }

    // Start is called before the first frame update
    void Start()
    {
        attention = player.position + 25f * player.forward;
    }

    void LateUpdate()
    {
        Vector3 CameraLocation = player.position + -10f * player.forward + 5f * Vector3.up;
        Camera.main.transform.position = (Camera.main.transform.position * cameraBias) + (CameraLocation * (1f - cameraBias));

        attention = (attention * cameraAttentionBias) + ((player.position + 25f * player.forward) * (1f - cameraAttentionBias));
        Camera.main.transform.LookAt(attention);
    }
}
