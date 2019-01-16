using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
// Made by Jocke
public class Movement : MonoBehaviour
{
    public InputMaster controls;

    private void Awake()
    {
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    void Move(Vector2 direction)
    {
        Debug.Log(direction);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
