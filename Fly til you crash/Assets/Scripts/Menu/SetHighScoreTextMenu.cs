using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetHighScoreTextMenu : MonoBehaviour
{
    public TextMeshProUGUI[] highscoreFields;
    public Highscores highscoresManager;

    void Start()
    {
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            highscoreFields[i].text = i + 1 + ". Fetching...";
        }
        StartCoroutine("RefreshHighscores");
    }

    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            highscoreFields[i].text = i + 1 + ". ";
            if (i < highscoreList.Length)
            {
                string[] test = highscoreList[i].username.Split('+');
                highscoreFields[i].text += test[0] + " - " + highscoreList[i].score;
            }
        }
    }

    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoresManager.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }
}
