using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollision : MonoBehaviour
{
    //Made by Philip Åkerblom GP18 Yrgo

    public bool isDead = false;
    public bool hasStartedCoroutine = false;
    public float waitForDestruction = 2.0f;
    public bool alreadyPlayed = false;
    UIController uiController;
    public static bool dead;

    private void Start()
    {
        uiController = FindObjectOfType<UIController>();
        dead = false;
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

            Time.timeScale = 0;

            if (hasStartedCoroutine == false)
            {
                StartCoroutine(WaitForDeath());
                hasStartedCoroutine = true;
                dead = true;
            }
        }
    }

    public IEnumerator WaitForDeath()
    {
        while (waitForDestruction > 0.0f)
        {
            waitForDestruction -= Time.deltaTime;
            StartCoroutine(FindObjectOfType<CameraShake>().Shake(.1f, .1f));
            yield return null;
        }

        if (waitForDestruction <= 0.0f)
        {
            isDead = true;
            uiController.backgroundImage.enabled = true;
            uiController.buttons.SetActive(true);
            uiController.userInputUI.SetActive(true);
            uiController.scoreUI.SetActive(false);
            uiController.inputeScoreText.text = "Score: " + UIController.score.ToString();
        }
        else
        {
            isDead = false;
            waitForDestruction = 1.0f;
        }
    }
}

