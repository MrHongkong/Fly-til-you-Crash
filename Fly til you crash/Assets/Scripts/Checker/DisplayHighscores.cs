using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DisplayHighscores : MonoBehaviour {

    //Made by Philip Åkerblom GP18 Yrgo
    //Code from Sebastian Lague

	public Text[] highscoreFields;
	Highscores highscoresManager;

	void Start() {
		for (int i = 0; i < highscoreFields.Length; i ++) {
			highscoreFields[i].text = i + 1 + ". Fetching...";
		}

        highscoresManager = GetComponent<Highscores>();
        StartCoroutine("RefreshHighscores");
	}
	
	public void OnHighscoresDownloaded(Highscore[] highscoreList) {
		for (int i = 0; i < highscoreFields.Length; i ++) {
			highscoreFields[i].text = i+1 + ". ";
			if (i < highscoreList.Length) {
                string[] test =  highscoreList[i].username.Split('+');
                highscoreFields[i].text += test[0] + " - " + highscoreList[i].score;
			}
		}
	}

    IEnumerator RefreshHighscores() {
		while (true) {
			highscoresManager.DownloadHighscores();
			yield return new WaitForSeconds(30);
		}
	}
}
