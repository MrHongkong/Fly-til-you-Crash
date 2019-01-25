using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour { 
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;
    float nextInc;

    

    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }
        transform.position = target.TransformPoint(offsetPosition);
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, target.transform.rotation, 5f * Time.deltaTime);
        //Camera.main.transform.LookAt(target, target.up);

    }
}
