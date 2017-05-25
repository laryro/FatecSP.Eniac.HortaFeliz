using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatematicaFase
{

    private int quantidadeSequencia, quantidadeItensReciclaveis;
    private float intervaloSons;
    private bool piscarItemReciclavel, piscarIconeSom;
    private const int MIN_QTD_SEQ = 2;
    private const int QTD_FASE_INCREMENTO_SEQ = 3;

    public MatematicaFase(int fase)
    {
        //presets Onload
        AtribuirPorFase(fase);
    }

    /*
         Nivel 0-5 : Papel e Plastico  | 3 segundos intervalo | pisca na tela | pisca item
         Nivel 6-10: +Vidro            | 2 segundos intervalo | pisca na tela | 
         Nivel 11-20: +Metal           | 1.5 segundo  intervalo | pisca na tela
         Nivel 21-99: Lixo Organico    | 1 segundo  intervalo |
         A cada 3 levels  tamanhoSequencia++
         */

    public void AtribuirPorFase(int fase)
    {
        SetQuantidadeSequencia(fase);
        SetQuantidadeItensReciclaveis(fase);
        SetIntervaloSons(fase);
        SetPiscarItemReciclavel(fase);
        SetPiscarIconeSom(fase);
    }


    #region Getters/Setters
    public void SetQuantidadeSequencia(int fase)
    {
        quantidadeSequencia = MIN_QTD_SEQ + (Convert.ToInt32(fase / QTD_FASE_INCREMENTO_SEQ));
    }
    public void SetQuantidadeItensReciclaveis(int fase)
    {
        if (fase <= 5)
        {
            quantidadeItensReciclaveis = 2;
        }
        else if (fase <= 10)
        {
            quantidadeItensReciclaveis = 3;
        }
        else if (fase <= 20)
        {
            quantidadeItensReciclaveis = 4;
        }
        else
        {
            quantidadeItensReciclaveis = 5;
        }
    }
    public void SetIntervaloSons(int fase)
    {
        if (fase <= 5)
        {
            intervaloSons = 3f;
        }
        else if (fase <= 10)
        {
            intervaloSons = 2f;
        }
        else if (fase <= 20)
        {
            intervaloSons = 1.5f;
        }
        else
        {
            intervaloSons = 1f;
        }
    }
    public void SetPiscarItemReciclavel(int fase)
    {
        if (fase < 5)
        {
            piscarItemReciclavel = true;
        }
        else
        {
            piscarItemReciclavel = false;
        }
    }
    public void SetPiscarIconeSom(int fase)
    {
        if (fase <= 20)
        {
            piscarIconeSom = true;
        }
        else
        {
            piscarIconeSom = false;
        }
    }
    public int ResultadoSoma { get; set; }
    public int GetQuantidadeSequencia()
    {
        return quantidadeSequencia;
    }
    public int GetQuantidadeItensReciclaveis()
    {
        return quantidadeItensReciclaveis;
    }
    public float GetIntervaloSons()
    {
        return intervaloSons;
    }
    public bool GetPiscarItemReciclavel()
    {
        return piscarItemReciclavel;
    }
    public bool GetPiscarIconeSom()
    {
        return piscarIconeSom;
    }
    #endregion
}
