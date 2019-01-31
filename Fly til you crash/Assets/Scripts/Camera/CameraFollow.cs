using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Vector3 offsetPosition;

    public float offsetPositionZ;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }
        offsetPositionZ = -8;
        offsetPosition = new Vector3(0, 2, offsetPositionZ);
    }

    private void LateUpdate()
    {
        TimeWarp();
        Refresh();
    }

    public void Refresh()
    {
        transform.position = target.TransformPoint(offsetPosition = new Vector3(0, 2, offsetPositionZ));
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, target.transform.rotation, 3f * Time.deltaTime);
    }

    public void TimeWarp()
    {
        if (Input.GetButton("TimeWarp"))
        {
            offsetPositionZ -= 0.1f;
            if (offsetPositionZ <= -12)
            {
                offsetPositionZ = -12;
            }
        }
        else
        {
            if (offsetPositionZ < -8)
            {
                offsetPositionZ += 0.2f;
            }
            else offsetPositionZ = -8f;
        }
        
    }
}
