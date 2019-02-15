using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Start()
    {
        StartCoroutine(ZoomInOnCar());
    }
    
    IEnumerator ZoomInOnCar()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        yield return waitForSeconds;
        while (!MenuVideoController.isPlaying)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, 1 * Time.deltaTime);
            yield return null;
        }
    }
}
