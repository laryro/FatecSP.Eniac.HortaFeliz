using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FundoController : MonoBehaviour {

    private float alturaTela,larguraTela;


	// Use this for initialization
	void Start () {
        SpriteRenderer grafico = GetComponent<SpriteRenderer>();

        float larguraImagem = grafico.sprite.bounds.size.x;
        float alturaImagem = grafico.sprite.bounds.size.y;

        //70% de transparencia, respeitando a cor do fundo da camera
        grafico.color = new Color(1f, 1f, 1f, 0.7f);
        
   
        alturaTela = Camera.main.orthographicSize * 2f +0.5f;
        //regra de 3 p/ obter a largura baseado na proporcao
        larguraTela = alturaTela / Screen.height * Screen.width +0.5f;
        //print(larguraTela);

        Vector2 novaEscala = transform.localScale;

        novaEscala.x = larguraTela / larguraImagem;
        novaEscala.y = alturaTela / alturaImagem;


        this.transform.localScale = novaEscala;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
