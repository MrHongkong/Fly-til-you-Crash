using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DisplayHighscores : MonoBehaviour {

    //Made by Philip Åkerblom GP18 Yrgo
    //Code from Sebastian Lague

	public TextMeshProUGUI[] highscoreFields;
	
    void OnEnable(){
        Highscores.DownloadHighscoresToTMPro(highscoreFields);
    }
}
