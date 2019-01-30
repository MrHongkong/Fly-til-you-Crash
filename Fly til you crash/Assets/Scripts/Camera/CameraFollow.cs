using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour { 
    [SerializeField]
    private Transform target;

    public Transform target2;

    [SerializeField]
    private Vector3 offsetPosition;


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

        if (target2 == null)
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, target.transform.rotation, 3f * Time.deltaTime);
        else
        
        {
            Vector3 tmp = Vector3.Lerp(target.position, target2.position, 0.5f);
            Camera.main.transform.LookAt(tmp, target.up);
            if (Vector3.Distance(target2.position, transform.position) < offsetPosition.magnitude)
            {
                target2 = null;
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            if (other.gameObject.name == "End")
            {
                target2 = other.gameObject.transform;
            }
        }
    }


}
