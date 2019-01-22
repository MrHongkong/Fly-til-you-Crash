using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OnCollision : MonoBehaviour
{
    //Made by Philip Åkerblom GP18 Yrgo

    bool isDead = false;
    float waitForDestruction = 2.0f;
    bool reloadScene = false;
    private Acceleration speed;
    //Rigidbody rgb;

    public void Start()
    {
        //rgb = GetComponent<Rigidbody>();
        speed = FindObjectOfType<Acceleration>();

    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Object" && isDead != true)
        {
            StartCoroutine(FindObjectOfType<CameraShake>().Shake(.1f, .1f));

            //GetComponent<MeshRenderer>().enabled = false;
            waitForDestruction -= Time.deltaTime;         
            Debug.Log("CountDown: " + waitForDestruction);


            speed.forwardVelocity = 0;

            if (waitForDestruction <= 0.0f)
            {
                isDead = true;

                if (isDead == true)
                {
                    reloadScene = true;

                    ReloadGame();
                }

                else
                {
                    Debug.Log("CountDown: " + waitForDestruction);
                    Debug.Log("Dead? " + isDead);
                    reloadScene = false;
                    isDead = false;
                    waitForDestruction = 2.0f;
                }
            }
        }

    }

    public void ReloadGame()
    {
        if (reloadScene == true)
        {     
            SceneManager.LoadScene("Level 1");
        }
    }

}
