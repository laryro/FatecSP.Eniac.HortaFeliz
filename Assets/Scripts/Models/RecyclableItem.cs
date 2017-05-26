using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecyclableItem
{



    public RecyclableItem(string nome, int valorItem, SpriteRenderer spriteItemRodape, SpriteRenderer cestaLixo, GameObject containerItem, Text qtdItem)
    {
        Nome = nome;
        SpriteItemRodape = spriteItemRodape;
        CestaLixo = cestaLixo;
        ContainerItem = containerItem;
        QuantidadeItem = qtdItem;
        ValorItem = valorItem;
        OcorrenciasItemRodada = 0;
    }


    public string Nome { get; set; }
    public SpriteRenderer SpriteItemRodape { get; set; }
    public SpriteRenderer CestaLixo { get; set; }
    public GameObject ContainerItem { get; set; }
    public Text QuantidadeItem { get; set; }
    public int ValorItem { get; set; }
    public int OcorrenciasItemRodada { get; set; }


}
