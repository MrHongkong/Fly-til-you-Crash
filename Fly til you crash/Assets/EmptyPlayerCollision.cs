using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPlayerCollision : MonoBehaviour
{
    public OnCollision onCollision;
    public CollisionParticleController collisionParticleController;

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        collisionParticleController.CollisionEnter(collision);
        onCollision.CollisionEnter(collision);
    }
}
