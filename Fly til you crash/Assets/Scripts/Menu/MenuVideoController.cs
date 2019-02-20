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
    public GameObject fields;
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
        if (videoPlayer3.time >= 3.5f)
        {
            quit.enabled = false;
            quitStatic.enabled = true;
            videoPlayer4.Play();
            SetVideoPlayerThreeToZeroPointOneAndPause();
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
        SetVideoPlayerTwoToZeroPointOneAndPause();
        videoPlayer3.Play();
        SetVideoPlayerThreeToZeroPointOneAndPause();
        videoPlayer4.Play();
        videoPlayer5.Play();
        SetVideoPlayerFiveToZeroPointOneAndPause();
        
        isPlaying = true;
        SetButtonsTrue();
    }

    public void SetVideoClip()
    {
        foreach (GameObject button in buttonController.buttonList)
        {

            if (button.activeInHierarchy && button.name == "Highscore" || button.activeInHierarchy && button.name == "Options")
            {   
                if(!isPause) HighscoreAndOptions();
            }

            if (button.activeInHierarchy && button.name == "New game")
            {
                if (!isPause)
                {
                    SetButtonClickToZeroPointSevenSeconds();
                    NewGame();
                }
                else
                {
                    fields.SetActive(false);
                    SetButtonsFalse();
                    PlayReversed();
                    SetButtonClickToThreeSeconds();
                    Invoke("NewGame", 3);
                }
            }

            if (button.activeInHierarchy && button.name == "Quit")
            {
                if (!isPause)
                {
                    SetButtonClickToZeroPointSevenSeconds();
                    Quit();
                }
                else
                {
                    SetButtonsFalse();
                    PlayReversed();
                    SetButtonClickToThreeSeconds();
                    Invoke("Quit", 3);
                }
            }
        }
    }

    void NewGame()
    {
        SetVideoPlayerTwoToZeroPointOneAndPause();
        SetVideoPlayerThreeToZeroPointOneAndPause();
        SetVideoPlayerFiveToZeroPointOneAndPause();
        highScore.enabled = false;
        quit.enabled = false;
        quitStatic.enabled = false;
        highScoreReversed.enabled = false;
        isPause = false;
        main.enabled = true;
        SetButtonsTrue();
    }

    void Quit()
    {
        SetVideoPlayerTwoToZeroPointOneAndPause();
        SetVideoPlayerFiveToZeroPointOneAndPause();
        isPause = false;
        highScore.enabled = false;
        quitStatic.enabled = false;
        main.enabled = false;
        highScoreReversed.enabled = false;
        quit.enabled = true;
        videoPlayer3.Play();
        SetButtonsTrue();
        SetFieldsFalse();
    }

    void HighscoreAndOptions()
    {
        SetVideoPlayerFiveToZeroPointOneAndPause();
        SetVideoPlayerThreeToZeroPointOneAndPause();
        isPause = false;
        main.enabled = false;
        quit.enabled = false;
        quitStatic.enabled = false;
        highScoreReversed.enabled = false;
        SetButtonsFalse();
        SetFieldsFalse();
        highScore.enabled = true;
        videoPlayer2.Play();
        SetButtonClickToZeroPointSevenSeconds();
        Invoke("HighscorePause", 3f);
    }

    void PlayReversed()
    {
        SetVideoPlayerTwoToZeroPointOneAndPause();
        SetVideoPlayerThreeToZeroPointOneAndPause();
        isPause = false;
        highScore.enabled = false;
        main.enabled = false;
        quit.enabled = false;
        quitStatic.enabled = false;
        SetButtonsFalse();
        SetFieldsFalse();
        highScoreReversed.enabled = true;
        videoPlayer5.Play();
    }

    void SetButtonsFalse()
    {
        buttons.SetActive(false);
    }

    void SetButtonsTrue()
    {
        buttons.SetActive(true);
    }

    void SetVideoPlayerTwoToZeroPointOneAndPause()
    {
        videoPlayer2.time = 0.1f;
        videoPlayer2.Pause();
    }

    void SetVideoPlayerThreeToZeroPointOneAndPause()
    {
        videoPlayer3.time = 0.1f;
        videoPlayer3.Pause();
    }
    void SetVideoPlayerFiveToZeroPointOneAndPause()
    {
        videoPlayer5.time = 0.1f;
        videoPlayer5.Pause();
    }
    
    void HighscorePause()
    {
        isPause = true;
        SetButtonsTrue();
        SetFieldsTrue();
        videoPlayer2.Pause();
    }
    
    void SetButtonClickToThreeSeconds()
    {
        SwapButtonRight.addSecondsToNextClick = 3f;
        SwapButtonLeft.addSecondsToNextClick = 3f;
    }

    void SetButtonClickToZeroPointSevenSeconds()
    {
        SwapButtonRight.addSecondsToNextClick = 0.7f;
        SwapButtonLeft.addSecondsToNextClick = 0.7f;
    }

    void SetFieldsTrue()
    {
        fields.SetActive(true);
    }

    void SetFieldsFalse()
    {
        fields.SetActive(false);
    }
}
