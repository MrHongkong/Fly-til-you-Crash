using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Made by Jocke
public class SwapButtonLeft : MonoBehaviour
{
    public MenuVideoController menuVideoController;
    ButtonController buttonController;
    public GameObject target;
    private float time;
    public float addSecondsToNextClick;

    private void Start()
    {
        buttonController = FindObjectOfType<ButtonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) Swap(-1);
    }

    public void MouseClick()
    {
        Swap(-1);
    }

    public void Swap(float swap)
    {
        if (Time.timeSinceLevelLoad > time)
        {
            time = Time.timeSinceLevelLoad + addSecondsToNextClick;
            buttonController.SwapButton(swap);
            menuVideoController.SetVideoClip();
            iTween.PunchScale(target, new Vector3(2, 2, 2), 0.4f);
        }
    }
}