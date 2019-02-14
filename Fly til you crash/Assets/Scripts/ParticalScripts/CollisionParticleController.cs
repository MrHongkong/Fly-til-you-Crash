using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticleController : MonoBehaviour
{
    public float cooldown;
    public float counter;

    public ParticleSystem sparks;

    void Start()
    {
        counter = cooldown * 1.1f;
        sparks.Pause();
    }

    public void CollisionEnter(Collision other){
        if (counter > cooldown){
            sparks.transform.position = other.GetContact(0).point;
            sparks.Play();
            counter = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (counter <= cooldown)
            counter += Time.deltaTime;
    }
}
