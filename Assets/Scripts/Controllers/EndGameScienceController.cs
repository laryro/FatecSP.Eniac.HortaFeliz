using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScienceController : MonoBehaviour {
	// Use this for initialization
	void Start () {
        string finalText = "Parabéns, você marcou " + PlayerPrefs.GetInt("scienceScore").ToString() + " pontos.";
        if (PlayerPrefs.GetInt("scienceScore") > PlayerPrefs.GetInt("scienceHighScore")) finalText += "\nVocê bateu o recorde!";

        gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text = finalText;
        gameObject.transform.Find("TxtRecordist").gameObject.GetComponent<Text>().text = "O Recorde atual é de " + PlayerPrefs.GetInt("scienceHighScore").ToString()+" pontos.";
	}
}
