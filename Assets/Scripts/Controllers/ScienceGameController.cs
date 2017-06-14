using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScienceGameController : MonoBehaviour
{
    public GameObject score, highestScore;

    // Use this for initialization
    void Start () {
            highestScore.GetComponent<Text>().text = "Recorde: " + PlayerPrefs.GetInt("scienceHighScore").ToString() + " pontos.";
    }
    
    public void SaveScore()
    {
        PlayerPrefs.SetInt("scienceScore", score.GetComponent<ScoreController>().GetScore());
        if( !PlayerPrefs.HasKey("scienceHighScore") || score.GetComponent<ScoreController>().GetScore() > PlayerPrefs.GetInt("scienceHighScore") )
            PlayerPrefs.SetInt("scienceHighScore", score.GetComponent<ScoreController>().GetScore());
    }
}
