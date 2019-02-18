using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Highscores : MonoBehaviour {

    //Code from Sebastian Lague
    
    const string privateCode = "gP34t5uH1kWaGuTw8kx51gOmQ-8rdCakyNOmII9cX8rg";
	const string publicCode = "5c4ac7d4b6397e0c24a5d87c";
	const string webURL = "http://dreamlo.com/lb/";

    public List<Highscore> highscoresList;
    public static Highscores instance;

    const int highscoreTextHolders = 10;

    void Start(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    
    IEnumerator UploadNewHighscore(string username, int score, int tries) {
        if (tries == 0){
            Debug.LogError("Failed to upload score after 3 attempts.");
            yield break;
        }
        
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error)){
            if (highscoresList.Count < highscoreTextHolders)
            {
                highscoresList.Add(new Highscore(username, score));
            }
            else
            {
                foreach (Highscore hs in highscoresList)
                {
                    if (score > hs.score)
                    {
                        highscoresList.Remove(hs);
                        highscoresList.Add(new Highscore(username, score));
                        break;
                    }
                }
            }

            highscoresList.Sort((x, y) => x.score.CompareTo(y.score));
            yield break;
        }

        yield return new WaitForSeconds(5f);
        StartCoroutine(UploadNewHighscore(username, score, tries - 1));
    }
    
	IEnumerator DownloadHighscoresFromDatabase() {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;
		
		if (string.IsNullOrEmpty (www.error)) {
            string[] entries = www.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            highscoresList = new List<Highscore>();

            for (int i = 0; i < entries.Length; i++){
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                highscoresList.Add(new Highscore(entryInfo[0], int.Parse(entryInfo[1])));
            }
        }
		else {
			print ("Error Downloading: " + www.error);
		}
	}

    IEnumerator _DownloadHighscoresToTMPro(TextMeshProUGUI[] highscoreFields){
        yield return DownloadHighscoresFromDatabase();

        for (int i = 0; i < highscoresList.Count; i++)
        {
            if (i >= highscoreFields.Length)
                break;

            highscoreFields[i].text = i + 1 + ". ";
            if (i < highscoresList.Count){
                string[] test = highscoresList[i].username.Split('+');
                highscoreFields[i].text += test[0] + " - " + highscoresList[i].score;
            }
        }
    }

    public static void AddNewHighscore(string username, int score){
        instance.StartCoroutine(instance.UploadNewHighscore(username, score, 3));
    }

    public static void DownloadHighscoresToTMPro(TextMeshProUGUI[] highscoreFields){
        instance.StartCoroutine(instance._DownloadHighscoresToTMPro(highscoreFields));
    }
}

public struct Highscore {
	public string username;
	public int score;

    public Highscore(string username, int score){
        this.username = username;
        this.score = score;
    }
}

