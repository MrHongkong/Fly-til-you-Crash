using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
//Made by Jocke
public class MenuVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public GameObject buttons;
    public GameObject car;
    public static bool isPlaying;
    void Start()
    {
        StartCoroutine(PlayVideo());
        car.SetActive(true);
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

    private void Update()
    {
        if (isPlaying)
        {
            car.SetActive(false);
            buttons.SetActive(true);
        }
    }

}
