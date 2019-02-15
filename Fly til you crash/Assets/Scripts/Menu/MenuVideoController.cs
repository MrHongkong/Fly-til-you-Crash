using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
//Made by Jocke
public class MenuVideoController : MonoBehaviour
{
    public VideoClip menuVideoClip;
    public VideoClip screenVideoClip;
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public GameObject buttons;
    public GameObject fields;
    public static bool isPlaying;
    public static bool isPlause;

    private ButtonController buttonController;

    void Start()
    {
        isPlaying = false;
        videoPlayer.clip = menuVideoClip;
        StartCoroutine(PlayVideo());
        buttonController = buttons.GetComponentInChildren<ButtonController>();
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(3);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        isPlaying = true;
    }

    IEnumerator InGamePlayVideo()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
        
        while (!videoPlayer.isPrepared)
        {
            isPlaying = false;
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        isPlaying = true;
    }

    private void LateUpdate()
    {
        if (isPlaying && videoPlayer.clip.name == "Car_interior_meny_v02") buttons.SetActive(true);
        else if (isPlause && videoPlayer.clip.name == "Car_menu_highscore_v01") buttons.SetActive(true);
        else buttons.SetActive(false);

        if (videoPlayer.clip.name == "Car_menu_highscore_v01" && videoPlayer.time == 3f)
        {
            videoPlayer.Pause();
            isPlause = true;
        }
    }

    public void SetVideoClip()
    {
        foreach (GameObject button in buttonController.buttonList)
        {
            if (button.activeInHierarchy && button.name == "Highscore" || button.activeInHierarchy && button.name == "Options")
            {
                videoPlayer.clip = screenVideoClip;
                videoPlayer.Prepare();
                StartCoroutine(InGamePlayVideo());
            }
            if (button.activeInHierarchy && button.name == "New game" || button.activeInHierarchy && button.name == "Quit")
            {
                videoPlayer.clip = menuVideoClip;
                videoPlayer.Prepare();
                isPlause = false;
                StartCoroutine(InGamePlayVideo());
            }
        }
    }
}
