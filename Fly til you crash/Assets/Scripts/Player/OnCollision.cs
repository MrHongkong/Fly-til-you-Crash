using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollision : MonoBehaviour
{
    //Made by Philip Åkerblom GP18 Yrgo

    public static bool isDead = false;
    public bool hasStartedCoroutine = false;
    public bool alreadyPlayed = false;
    float waitForDeath;
    private void Start()
    {
        isDead = false;
    }

    private void Update()
    {
        if (isDead && Time.timeSinceLevelLoad > waitForDeath) WaitForDeath();
    }

    public void CollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Object" && isDead != true)
        {
            if (!alreadyPlayed)
            {
                FindObjectOfType<AudioManager>().Stop("CarSound");
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                alreadyPlayed = true;
            }

            if (!isDead)
            {
                isDead = true;
                waitForDeath = Time.timeSinceLevelLoad + 1.5f;
            }
        }
    }

    public void WaitForDeath()
    {
        if (isDead) SceneManager.LoadScene("ScoreScreen");
    }    
}

