using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInstantiatorController : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;

    public GameObject car1Camera;
    public GameObject car2Camera;

    public string cameraTargetName;

    // Start is called before the first frame update
    void Start()
    {
        if (!MenuSettings.getCarInUse())
        {
            GameObject car1Instance = Instantiate(car1);
            GameObject car1CameraInstance = Instantiate(car1Camera);

            CameraController cc = car1CameraInstance.GetComponent<CameraController>();
            cc.player = GameObject.Find(cameraTargetName).transform;

            MonoBehaviour[] components = GameObject.Find(cameraTargetName).GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in components)
                c.enabled = true;
            MonoBehaviour[] cameracomponents = car1CameraInstance.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in cameracomponents)
                c.enabled = true;
        }
        else
        {
            GameObject car2Instance = Instantiate(car2);
            GameObject car2CameraInstance = Instantiate(car2Camera);

            CameraController cc = car2CameraInstance.GetComponent<CameraController>();
            cc.player = GameObject.Find(cameraTargetName).transform;

            MonoBehaviour[] components = GameObject.Find(cameraTargetName).GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in components)
                c.enabled = true;
            MonoBehaviour[] cameracomponents = car2CameraInstance.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in cameracomponents)
                c.enabled = true;
        }

        Destroy(gameObject);
    }

    
}
