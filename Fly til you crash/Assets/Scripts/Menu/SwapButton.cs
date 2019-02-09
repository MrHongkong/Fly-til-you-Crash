using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Made by Jocke
public class SwapButton : MonoBehaviour
{
    ButtonController buttonController;
    public GameObject target;
    private float time;
    public float addSecondsToNextClick;
    public float swapLeftOrRight;

    private void Start()
    {
        buttonController = FindObjectOfType<ButtonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Swap(swapLeftOrRight);
        }
    }

    public void MouseClick()
    {
        Swap(swapLeftOrRight);
    }

    public void Swap(float swap)
    {
        if (Time.timeSinceLevelLoad > time)
        {
            time = Time.timeSinceLevelLoad + addSecondsToNextClick;
            buttonController.SwapButton(swap);
            iTween.PunchScale(target, new Vector3(2, 2, 2), 0.4f);
        }
    }
}