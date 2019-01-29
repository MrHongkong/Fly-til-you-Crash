using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPlayerCollision : MonoBehaviour
{
    public OnCollision onCollision;

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        onCollision.CollisionEnter(collision);
    }
}
