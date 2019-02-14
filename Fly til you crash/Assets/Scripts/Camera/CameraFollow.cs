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
        offsetPositionZ = -6;
        offsetPosition = new Vector3(0, 2, offsetPositionZ);
    }

    private void LateUpdate()
    {
        TimeWarp();
        Refresh();
    }

    public void Refresh()
    {
        offsetPosition = new Vector3(0, 2, offsetPositionZ);
        Vector3 wantedPosition = target.TransformPoint(offsetPosition);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, 3f * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, 3f * Time.deltaTime);
    }

    public void TimeWarp()
    {
        if (Input.GetButton("TimeWarp"))
        {
            offsetPositionZ -= 0.1f;
            if (offsetPositionZ <= -10)
            {
                offsetPositionZ = -10;
            }
        }
        else
        {
            if (offsetPositionZ < -6)
            {
                offsetPositionZ += 0.2f;
            }
            else offsetPositionZ = -6f;
        }
        
    }
}
