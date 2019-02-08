using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapLeftButton : MonoBehaviour
{
    ButtonController buttonController;
    public GameObject target;
    
    private void Start()
    {
        buttonController = FindObjectOfType<ButtonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            buttonController.SwapButton(-1);
            iTween.PunchScale(target, new Vector3(2,2,2), 0.7f);
        }
    }

    public void MouseClick()
    {
        buttonController.SwapButton(-1);
        iTween.PunchScale(target, new Vector3(2, 2, 2), 0.7f);
    }
}
