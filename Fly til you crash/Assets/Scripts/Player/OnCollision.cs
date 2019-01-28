using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OnCollision : MonoBehaviour
{
    //Made by Philip Åkerblom GP18 Yrgo

    public bool isDead = false;
    public bool hasStartedCoroutine = false;
    public float waitForDestruction = 2.0f;
    public bool reloadScene = false;
    Username user;

    public void Start()
    {
        user = FindObjectOfType<Username>();
        user.transform.parent.gameObject.SetActive(false);

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Object" && isDead != true)
        {
            if (hasStartedCoroutine == false)
            {
                StartCoroutine(WaitForDeath());
                hasStartedCoroutine = true;    
            }
            else
            {
                
            }

        }

    }

    public IEnumerator WaitForDeath()
    {
        while(waitForDestruction > 0.0f)
        {
            waitForDestruction -= Time.deltaTime;
            StartCoroutine(FindObjectOfType<CameraShake>().Shake(.1f, .1f));
            Debug.Log("SHAKING");
            yield return null;
        }

        if (waitForDestruction <= 0.0f)
        {
            isDead = true;

            if (isDead == true)
            {

                Time.timeScale = 0;

                if (user == null)
                {
                    Debug.LogError("ERROR: user is null");
                }
                else if (user.UserInputUI == null)
                {
                    Debug.LogError("ERROR: user.UserInputUI is null");
                }
                else
                {
                    user.UserInputUI.SetActive(true);
                }
                reloadScene = true;
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

    public void ReloadGame()
    {
        if (reloadScene == true)
        {

            SceneManager.LoadScene("MVP");
        }
    }

}
