using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Made by Jocke
public class Movement : MonoBehaviour
{
    //public InputMaster controls;
    public Rigidbody rb;

    private void Awake()
    {
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    void Move(Vector2 direction)
    {
        Vector2 tmp = new Vector2(transform.position.x, transform.position.y);
        Vector2 move = tmp + direction;
        rb.MovePosition(move);
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
