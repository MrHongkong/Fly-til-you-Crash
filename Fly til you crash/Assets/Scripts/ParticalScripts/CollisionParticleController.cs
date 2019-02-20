using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticleController : MonoBehaviour
{
    public float cooldown;
    public float counter;

    public List<ParticleSystem> particleSystems;

    void Start()
    {
        counter = cooldown * 1.1f;
        foreach (ParticleSystem ps in particleSystems)
            ps.Pause();
    }

    public void CollisionEnter(Collision other){
        if (counter > cooldown){
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.transform.position = other.GetContact(0).point + (Camera.main.transform.position - transform.position) * 0.3f;
                ps.Play();
            }
            counter = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (counter <= cooldown)
            counter += Time.deltaTime;
        else
            foreach (ParticleSystem ps in particleSystems)
                ps.Pause();
    }
}
