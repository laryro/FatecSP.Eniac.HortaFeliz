
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{

    //Pontuacao do jogador por disciplina
    private int ptsMatematica, ptsIngles, ptsCiencias, ptsPortugues;
    private int nivelMatematica,tempoJogoMatematica;
    private string nomeJogadorAtual;




    // Use this for initialization
    void Start()
    {
        ptsMatematica = ptsPortugues = ptsCiencias = ptsIngles = nivelMatematica = 0;
        InitPlayerPreferences();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        PlayerPrefs.Save();
    }

    void InitPlayerPreferences()
    {
        PlayerPrefs.SetInt("ptsCiencias", ptsCiencias);
        PlayerPrefs.SetInt("ptsIngles", ptsIngles);
        PlayerPrefs.SetInt("ptsMatematica", ptsMatematica);
        PlayerPrefs.SetInt("ptsPortugues", ptsPortugues);
        PlayerPrefs.SetInt("nivelMatematica", nivelMatematica);
        PlayerPrefs.SetInt("tempoJogoMatematica", tempoJogoMatematica);
        
    }


}
