using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapRightButton : MonoBehaviour
{
    ButtonController buttonController;
    public GameObject target;

    private void Start()
    {
        buttonController = FindObjectOfType<ButtonController>();    
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            buttonController.SwapButton(1);
            iTween.PunchScale(target, new Vector3(2, 2, 2), 0.4f);
            iTween.PunchRotation(target, new Vector3(0, 0, 50), 0.4f);
        }
    }

    public void MouseClick()
    {
        buttonController.SwapButton(1);
        iTween.PunchScale(target, new Vector3(2, 2, 2), 0.4f);
        iTween.PunchRotation(target, new Vector3(0, 0, 50), 0.4f);
    }
}
