using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour {

    //Made by Philip Åkerblom GP18 Yrgo
    //Code from Sebastian Lague
    //public Text[] usernameFields;
	public Text[] highscoreFields;
	Highscores highscoresManager;

	void Start() {
		for (int i = 0; i < highscoreFields.Length; i ++) {
			highscoreFields[i].text = i+1 + ". Fetching...";
		}

        //for (int i = 0; i < usernameFields.Length; i++)
        //{
        //    usernameFields[i].text = i + 1 + ". Fetching...";
        //}

        highscoresManager = GetComponent<Highscores>();
        StartCoroutine("RefreshHighscores");
	}
	
	public void OnHighscoresDownloaded(Highscore[] highscoreList) {
		for (int i =0; i < highscoreFields.Length; i ++) {
			highscoreFields[i].text = i+1 + ". ";
			if (i < highscoreList.Length) {
				highscoreFields[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
			}
		}
	}

    //public void OnUsernameDownloaded(Usernames[] usernameList)
    //{
    //    for (int i = 0; i < usernameFields.Length; i++)
    //    {
    //        usernameFields[i].text = i + 1 + ". ";
    //        if (i < usernameList.Length)
    //        {
    //            highscoreFields[i].text += usernameList[i].username + " - " + usernameList[i].score;
    //        }
    //    }
    //}

    IEnumerator RefreshHighscores() {
		while (true) {
			highscoresManager.DownloadHighscores();
			yield return new WaitForSeconds(30);
		}
	}
}
