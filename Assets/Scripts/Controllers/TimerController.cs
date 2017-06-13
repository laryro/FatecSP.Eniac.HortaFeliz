using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    public Text timerValue;


    float timeLeft = 15.0F; //2 Minutes

    void Start()
    {

    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

		
        string minutes = Mathf.Floor(timeLeft / 60) < 10 ? "0" + Mathf.Floor(timeLeft / 60) : Mathf.Floor(timeLeft / 60).ToString();
        string seconds = Mathf.Round(timeLeft % 60) < 10 ? "0" + Mathf.Round(timeLeft % 60) : Mathf.Round(timeLeft % 60).ToString();
        if (seconds.Equals("60")) { seconds = "59";}
		if ( int.Parse(seconds) <= 10)
			timerValue.color = new Color (1f, 0f, 0f);
		
        timerValue.text =  minutes+ ":" +seconds ;
        //EndGame
        if (timeLeft <= 1)
        {
            SceneManager.LoadScene("Game");
            /*TODO Get HighScoreFunction

        public void GravaPontuacaoMaxima()
    {
        PlayerPrefs.SetInt("pontuacaoMaximaMatematica", PlayerPrefs.GetInt("ptsMatematica"));
        PlayerPrefs.SetString("nomeVencedorMatematica", inputNomeVencedor.text);
        EncerrarPartida();
    }
    public void EncerrarPartida()
    {
        //grava highscore e nome vai para o pre jogo ou p/ menu principal
        PlayerPrefs.SetInt("nivelMatematica", 0);
        PlayerPrefs.SetInt("ptsMatematica", 0);
        SceneManager.LoadScene("MainMenu");
    }
             */
        }
    }
}
