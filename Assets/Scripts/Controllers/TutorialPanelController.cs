using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanelController : MonoBehaviour
{

    private TutorialPanel painel;
    public Text textoDescritivo;

    #region Matematica
    public AudioClip somPapel, somPlastico, somMetal, somVidro, somOrganico, somDescoberta;
    public SpriteRenderer spritePapel, spriteVidro, spritePlastico, spriteMetal, spriteOrganico;
    public Text textoPapel, textoVidro, textoPlastico, textoMetal, textoOrganico;
    public GameObject botaoPapel, botaoVidro, botaoPlastico, botaoMetal, botaoOrganico;
    public GameObject groupAdvise, tutorialPanel, groupHome, groupSample, groupDemo, groupRecycable;
    public Button botaoJogar;
    #endregion

    // Use this for initialization
    void Start()
    {

        switch (this.tag)
        {
            case "Ciencias":
                // Not developed yet.
                break;
            case "Ingles":
                // Not developed yet.
                break;
            case "Matematica":
                spriteMetal.enabled = spriteOrganico.enabled = spritePlastico.enabled = spritePapel.enabled = spriteVidro.enabled = false;
                textoMetal.enabled = textoOrganico.enabled = textoPlastico.enabled = textoPapel.enabled = textoVidro.enabled = false;
                botaoMetal.SetActive(false); botaoOrganico.SetActive(false); botaoPlastico.SetActive(false); botaoPapel.SetActive(false); botaoVidro.SetActive(false);
                CarregarTutorialMatematica();
                break;
            case "Portugues":
               CarregarTutorialPortugues();
                break;
            default:
                print("Painel sem Tag de Tema");
                break;
        }

    }

    void OnDestroy()
    {
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void CarregarTutorialPortugues()
    {
        ShowTutorialPtStep();
    }

        private void CarregarTutorialMatematica()
    {
        painel = new TutorialPanel();
        //Esconde a demo
        groupDemo.SetActive(false);
        //groupRecycable.SetActive(true);
        #region SelecaoMensagem
        /*
         Nivel 0-5 : Papel e Plastico  | 3 segundos intervalo | pisca na tela | pisca item
         Nivel 6-10: +Vidro            | 2 segundos intervalo | pisca na tela | 
         Nivel 11-20: +Metal           | 1 segundo  intervalo | pisca na tela
         Nivel 21-99: Lixo Organico    | 1 segundo  intervalo |
         A cada 3 levels  tamanhoSequencia++
         */
        switch (PlayerPrefs.GetInt("nivelMatematica"))
        {
            default:
                painel.MostraMetal = painel.MostraOrganico = painel.MostraPapel = painel.MostraVidro = painel.MostraPlastico = true;
                ShowAdvise();
                break;
            //Tutorial Vidro
            case 6:
                groupSample.SetActive(true);
                painel.TextoDescritivo = @"Você desbloqueou um novo item: Vidro.
Tipos de vidro recicláveis : 
Garrafas de sucos, refrigerantes, cervejas e outros tipos de bebidas;
Potes de alimentos, Cacos de vidros, Frascos de remédios

Preste atenção  no som do Vidro:
			";
                painel.MostraVidro = true;
                tutorialPanel.SetActive(true);
                GetComponent<AudioSource>().PlayOneShot(somDescoberta);
                break;
            //Tutorial Metal
            case 11:
                groupSample.SetActive(true);
                tutorialPanel.SetActive(true);
                painel.TextoDescritivo = @"Você desbloqueou um novo item: Metal.
Tipos de metais recicláveis : Latas de alumínio (refrigerante, cerveja, etc.) e aco (latas de sardinha, molhos, óleo, etc.

<b>O intervalo entre os sons ficará menor. Fique atento.
Preste atencao  no som do Metal:</b>
			";
                painel.MostraMetal = true;
                GetComponent<AudioSource>().PlayOneShot(somDescoberta);
                break;
            case 21:
                groupSample.SetActive(true);
                tutorialPanel.SetActive(true);
                painel.TextoDescritivo = @"Você desbloqueou um novo item: Lixo Orgânico. 
Podemos citar como exemplos de lixo orgânico: restos de alimentos orgânicos (carnes, vegetais, frutos, cascas de ovos), ossos, sementes, etc.

A Partir de agora o jogo ficará muito mais difícil! 
				Preste atencao no som do Lixo Orgânico:
			";
                GetComponent<AudioSource>().PlayOneShot(somDescoberta);
                painel.MostraOrganico = true;
                break;
            

        }
        #endregion
        textoDescritivo.text = painel.TextoDescritivo;
        spriteMetal.enabled = textoMetal.enabled = painel.MostraMetal;
        spriteOrganico.enabled = textoOrganico.enabled = painel.MostraOrganico;
        spritePlastico.enabled = textoPlastico.enabled = painel.MostraPlastico;
        spritePapel.enabled = textoPapel.enabled = painel.MostraPapel;
        spriteVidro.enabled = textoVidro.enabled = painel.MostraVidro;
        botaoMetal.SetActive(painel.MostraMetal);
        botaoOrganico.SetActive(painel.MostraOrganico);
        botaoPlastico.SetActive(painel.MostraPlastico);
        botaoPapel.SetActive(painel.MostraPapel);
        botaoVidro.SetActive(painel.MostraVidro);
    }


    //Toca o audio de cada som Solicitado
    public void TocarSom(string nomeSom)
    {
        //É Obrigatório associar um componente do tipo Audio Source
        switch (nomeSom)
        {
            case "plastico":
                GetComponent<AudioSource>().PlayOneShot(somPlastico);
                break;
            case "papel":
                GetComponent<AudioSource>().PlayOneShot(somPapel);
                break;
            case "metal":
                GetComponent<AudioSource>().PlayOneShot(somMetal);
                break;
            case "vidro":
                GetComponent<AudioSource>().PlayOneShot(somVidro);
                break;
            case "organico":
                GetComponent<AudioSource>().PlayOneShot(somOrganico);
                break;
        }
    }

    public void ShowAdvise()
    {
        tutorialPanel.SetActive(false);
        groupAdvise.SetActive(true);
    }



    public void ShowTutorialPtStep()
    {
        tutorialPanel.SetActive(true);
        groupHome.SetActive(true);
        textoDescritivo.text = @"Nome do Jogo : Jogo dos Plurais

A cada plural acertado você ganha pontos !!!!
Uma tentiva  = 10 Pontos
Duas Tentaivas  = 5 Pontos
Três tentativas = 2 Pontos
Quatro em diante  = 1 Ponto
Caso você precise de ajuda click em ver resposta.
Mas Atenção ao ver as respontas você não ganhará pontos.";
    }

    public void ShowTutorialFirstStep()
    {
        groupAdvise.SetActive(false);
        tutorialPanel.SetActive(true);
        groupHome.SetActive(true);
        groupRecycable.SetActive(false);
        textoDescritivo.text = @"Nome do Jogo : Soma Musical

Você ouvirá uma sequência de sons dos materiais recicláveis. 
Cada um deles equivale a um número. 

Após ouvir toda a sequência de sons, você deverá responder qual é o resultado da soma correspondente aos sons ouvidos.";
    }

    public void ShowTutorialSample()
    {
        groupHome.SetActive(false);
        tutorialPanel.SetActive(true);
        groupSample.SetActive(true);
        groupDemo.SetActive(true);
        groupRecycable.SetActive(true);
        botaoJogar.enabled = true;
        textoDescritivo.text = @"Exemplo
Som de papel = 3                 Som de plastico = 1                   

 
Aproveite esse momento e ouça bem cada um dos sons antes de comecar o jogo.
    Boa Sorte!";
    }


}
