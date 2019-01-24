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

    float fov;
    
    private void Start()
    {
        fov = 60;
    }
    private void LateUpdate()
    {
        FakeSpeed();
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
        
    }

    public void FakeSpeed()
    {
        float nextInc = 0;
        Debug.Log("FoV " + fov + " nextInc " + nextInc + " Time " + Time.time);
        Debug.Log(Camera.main.fieldOfView);
        if (Time.time < nextInc)
        {
            nextInc = Time.time + 3;
            if (fov < 120 && Time.time < nextInc)
            {
                Camera.main.fieldOfView = fov;
                fov++;
                Debug.Log(Camera.main.fieldOfView);
            }
        
        } 
    }
}
