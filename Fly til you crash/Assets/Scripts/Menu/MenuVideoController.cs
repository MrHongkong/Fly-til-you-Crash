using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
//Made by Jocke
public class MenuVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public VideoPlayer videoPlayer3;
    public VideoPlayer videoPlayer4;
    public VideoPlayer videoPlayer5;
    public RawImage main;
    public RawImage highScore;
    public RawImage quit;
    public RawImage quitStatic;
    public RawImage highScoreReversed;
    public GameObject buttons;
    public static bool isPlaying;
    public static bool isPause;

    private ButtonController buttonController;

    void Start()
    {
        quit.enabled = false;
        quitStatic.enabled = false;
        highScore.enabled = false;
        highScoreReversed.enabled = false;
        isPlaying = false;
        isPause = false;
        StartCoroutine(PlayVideo());
        buttonController = buttons.GetComponentInChildren<ButtonController>();
    }

    private void Update()
    {
        if (videoPlayer2.time >= 3f)
        {
            isPause = true;
            buttons.SetActive(true);
            videoPlayer2.Pause();
        }

        if (videoPlayer3.time >= 3.5f)
        {
            quit.enabled = false;
            quitStatic.enabled = true;
            videoPlayer4.Play();
            videoPlayer3.time = 0.1f;
            videoPlayer3.Pause();
        }
    }


    IEnumerator PlayVideo()
    {
        videoPlayer1.Prepare();
        videoPlayer2.Prepare();
        videoPlayer3.Prepare();
        videoPlayer4.Prepare();
        videoPlayer5.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(3);
        while (!videoPlayer1.isPrepared && !videoPlayer2.isPrepared && !videoPlayer3.isPrepared && !videoPlayer4.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        main.texture = videoPlayer1.texture;
        highScore.texture = videoPlayer2.texture;
        quit.texture = videoPlayer3.texture;
        quitStatic.texture = videoPlayer4.texture;
        highScoreReversed.texture = videoPlayer5.texture;
        videoPlayer1.Play();
        videoPlayer2.Play();
        videoPlayer2.Pause();
        videoPlayer3.Play();
        videoPlayer3.Pause();
        videoPlayer4.Play();
        videoPlayer5.Play();
        videoPlayer5.Pause();
        
        isPlaying = true;
        buttons.SetActive(true);
    }

    public void SetVideoClip()
    {
        foreach (GameObject button in buttonController.buttonList)
        {

            if (button.activeInHierarchy && button.name == "Highscore" || button.activeInHierarchy && button.name == "Options")
            {
                videoPlayer5.time = 0.1f;
                videoPlayer3.time = 0.1f;
                videoPlayer3.Pause();
                main.enabled = false;
                quit.enabled = false;
                quitStatic.enabled = false;
                highScoreReversed.enabled = false;
                buttons.SetActive(false);
                highScore.enabled = true;
                videoPlayer2.Play();
            }

            if (button.activeInHierarchy && button.name == "New game")
            {
                Debug.Log("isPause state " + isPause);
                if (isPause)
                {
                    StartCoroutine(PlayReveredVideo());
                }
                else NewGame();
            }

            if (button.activeInHierarchy && button.name == "Quit")
            {
                Debug.Log("Quit");
                buttons.SetActive(true);
                videoPlayer2.time = 0.1f;
                videoPlayer2.Pause();
                highScore.enabled = false;
                quitStatic.enabled = false;
                isPause = false;
                Debug.Log("Quit isPause sets to " + isPause);
                main.enabled = false;
                quit.enabled = true;
                highScoreReversed.enabled = false;
                videoPlayer3.Play();
            }
        }
    }

    IEnumerator PlayReveredVideo()
    {
        videoPlayer5.Play();
        buttons.SetActive(false);
        highScore.enabled = false;
        highScoreReversed.enabled = true;
        yield return new WaitForSeconds(3f);
        NewGame();
    }

    void NewGame()
    {
        buttons.SetActive(true);
        videoPlayer2.time = 0.1f;
        videoPlayer3.time = 0.1f;
        videoPlayer2.Pause();
        highScore.enabled = false;
        quit.enabled = false;
        quitStatic.enabled = false;
        highScoreReversed.enabled = false;
        isPause = false;
        main.enabled = true;
    }
}
