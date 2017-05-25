using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatematicaFaseController : MonoBehaviour
{

    #region Interfaces GUI
    public GameObject grupoPreJogo, grupoTocarSequencia, grupoAguardarResposta, grupoMostrarResultado, grupoHighScore;
    public SpriteRenderer iconeAudio;
    public SpriteRenderer rodapePapel, rodapePlastico, rodapeVidro, rodapeMetal, rodapeOrganico;
    public SpriteRenderer cestaPapel, cestaPlastico, cestaVidro, cestaMetal, cestaOrganico;
    public GameObject containerPapel, containerPlastico, containerVidro, containerMetal, containerOrganico;
    public Image mostraResultadoPainel;
    public Button proximaRodada, irParaMenuPrincipal;
    public Text txtQtdPapel, txtQtdPlastico, txtQtdVidro, txtQtdMetal, txtQtdOrganico;
    public Text txtPontuacao, txtMensagemResultado, txtBtnProximaRodada, txtRecordista;
    public AudioClip somPapel, somPlastico, somMetal, somVidro, somOrganico;
    public AudioClip somAcerto, somErro;
    #endregion

    private MatematicaFase matematica;
    private const int VLR_PAPEL = 3;
    private const int VLR_PLASTICO = 1;
    private const int VLR_VIDRO = 2;
    private const int VLR_METAL = 4;
    private const int VLR_ORGANICO = 9;
    private const int MULTIPLICADOR_PONTOS = 50;
    private List<ItemReciclavel> listaItens;
    private List<Int32> listaSelecionados;
    public InputField inputResultado, inputNomeVencedor;

    // Use this for initialization
    void Start()
    {
        //Instancia uma fase do jogo de matematica de acordo com o Level do Jogador
        matematica = new MatematicaFase(PlayerPrefs.GetInt("nivelMatematica"));
        listaItens = new List<ItemReciclavel>(5);
        InicializaListaItens();
        PreJogo();

    }

    // Update is called once per frame
    void Update()
    {

    }

     void OnDestroy()
    {
        PlayerPrefs.Save();
    }

    private void InicializaListaItens()
    {
        listaItens.Add(new ItemReciclavel("Papel", VLR_PAPEL, rodapePapel, cestaPapel, containerPapel, txtQtdPapel));
        listaItens.Add(new ItemReciclavel("Plastico", VLR_PLASTICO, rodapePlastico, cestaPlastico, containerPlastico, txtQtdPlastico));
        listaItens.Add(new ItemReciclavel("Vidro", VLR_VIDRO, rodapeVidro, cestaVidro, containerVidro, txtQtdVidro));
        listaItens.Add(new ItemReciclavel("Metal", VLR_METAL, rodapeMetal, cestaMetal, containerMetal, txtQtdMetal));
        listaItens.Add(new ItemReciclavel("Organico", VLR_ORGANICO, rodapeOrganico, cestaOrganico, containerOrganico, txtQtdOrganico));
    }

    public void PreJogo()
    {
        //carrega botao de play
        grupoPreJogo.SetActive(true);

        txtPontuacao.text = PlayerPrefs.GetInt("ptsMatematica").ToString();
        txtRecordista.text = "Recordista: " + PlayerPrefs.GetString("nomeVencedorMatematica") + " : " + PlayerPrefs.GetInt("pontuacaoMaximaMatematica") + " PTS";


        foreach (var item in listaItens)
        {
            item.OcorrenciasItemRodada = 0;
            item.CestaLixo.enabled = false;
            item.ContainerItem.SetActive(false);
        }

        //Acrescenta a dificuladade
        matematica.AtribuirPorFase(GetNivel());
    }

    //Devera desabiltar interacao
    public void TocarSequencia()
    {
        //desliga interface pre-jogo e carrega interface da sequencia
        grupoPreJogo.SetActive(false);
        grupoTocarSequencia.SetActive(true);
        GerarSequencia();

    }

    public void GerarSequencia()
    {
        //Inicializa sequencia
        int resultSoma = 0;
        listaSelecionados = new List<Int32>();

        //Incrementa quantidade de intes para dificultar com o tempo
        int qtdItemSequencia = matematica.GetQuantidadeSequencia();

        for (int i = 0; i < qtdItemSequencia; i++)
        {
            int indiceItem = ObterItemALeatorio();
            //Adiciona valor do item na soma final
            resultSoma += listaItens[indiceItem].ValorItem;
            //Guarda a quantidade de ocorrencias para mostrar no final
            listaItens[indiceItem].OcorrenciasItemRodada++;
            //ColocaItem na fila
            listaSelecionados.Add(indiceItem);
        }

        matematica.ResultadoSoma = resultSoma;
        for (int i = 0; i < qtdItemSequencia; i++)
        {
            //Apos carregar a lista dos selecionados
            //Chama a função que toca o som / pisca o som / pisca o item
            Invoke("TocaSom", 1 + (i * matematica.GetIntervaloSons()));
        }
    }

    public void TocaSom()
    {
        //É Obrigatório associar um componente do tipo Audio Source


        int indiceAtual = listaSelecionados[0];


        switch (indiceAtual)
        {
            case 0:
                GetComponent<AudioSource>().PlayOneShot(somPapel);
                break;
            case 1:
                GetComponent<AudioSource>().PlayOneShot(somPlastico);
                break;
            case 2:
                GetComponent<AudioSource>().PlayOneShot(somVidro);
                break;
            case 3:
                GetComponent<AudioSource>().PlayOneShot(somMetal);
                break;
            case 4:
                GetComponent<AudioSource>().PlayOneShot(somOrganico);
                break;
        }

        listaSelecionados.RemoveAt(0);


        if (listaSelecionados.Count == 0)
        {
            //Aguarda um segundo para mostra a mensagem
            Invoke("AguardarResposta", 1.5f);
        }

    }

    /// <summary>
    /// Obtem um indice de acordo com o tamanho da lista
    /// </summary>
    /// <returns></returns>
    private int ObterItemALeatorio()
    {

        int qtdSecoes = matematica.GetQuantidadeItensReciclaveis();
        int indiceItemEscolhido = 0;
        float rangeSecao = 1f / Convert.ToSingle(qtdSecoes);
        float numAleatorio = UnityEngine.Random.value;
        float limitador = rangeSecao;

        for (int i = 0; i < qtdSecoes; i++)
        {
            if (numAleatorio <= limitador)
            {
                indiceItemEscolhido = i;
                break;
            }
            limitador += rangeSecao;
        }
        return indiceItemEscolhido;
    }

    public void AguardarResposta()
    {
        grupoTocarSequencia.SetActive(false);
        grupoAguardarResposta.SetActive(true);
    }

    public void MostrarResultado()
    {
        grupoAguardarResposta.SetActive(false);
        grupoMostrarResultado.SetActive(true);
        int resultadoJogador = 0;
        Int32.TryParse(inputResultado.text, out resultadoJogador);
        inputResultado.text = string.Empty;

        String textoSentenca = "";
        foreach (var item in listaItens)
        {
            if (item.OcorrenciasItemRodada > 0)
            {
                item.QuantidadeItem.text = item.OcorrenciasItemRodada.ToString() + "X";
                item.CestaLixo.enabled = true;
                item.ContainerItem.SetActive(true);
                for (int i = 0; i < item.OcorrenciasItemRodada; i++)
                {
                    textoSentenca += " " + item.ValorItem + "+";
                }
            }
        }
        //retira o ultimo sinal de +
        textoSentenca = textoSentenca.Substring(0, textoSentenca.Length - 1) + " = " + matematica.ResultadoSoma;


        if (resultadoJogador == matematica.ResultadoSoma)
        {
            txtMensagemResultado.text = "Parabéns! " + textoSentenca;
            PlayerPrefs.SetInt("ptsMatematica", PlayerPrefs.GetInt("ptsMatematica") + (matematica.GetQuantidadeSequencia() * MULTIPLICADOR_PONTOS));
            CarregaAcerto();
        }
        else
        {
            txtMensagemResultado.text = "Você errou! Veja a sentença: " + textoSentenca;
            CarregaErro();
        }
    }

    public void CarregaAcerto()
    {
        mostraResultadoPainel.color = new Color(0.16f, 0.77f, 0.16f);
        proximaRodada.tag = "Ganhou";
        txtBtnProximaRodada.text = "Próxima Rodada";
        GetComponent<AudioSource>().PlayOneShot(somAcerto);
        PlayerPrefs.SetInt("nivelMatematica", (PlayerPrefs.GetInt("nivelMatematica") + 1));
    }

    public void CarregaErro()
    {
        mostraResultadoPainel.color = new Color(1f, 0.5f, 0.5f);
        GetComponent<AudioSource>().PlayOneShot(somErro);
        proximaRodada.tag = "Perdeu";
        txtBtnProximaRodada.text = "Encerrar";
    }

    public void NovaRodada()
    {




        if (proximaRodada.tag.Equals("Ganhou"))
        {
            if (GetNivel() == 6 || GetNivel() == 11 || GetNivel() == 21)
            {
                SceneManager.LoadScene("Matematica_Tutorial");
            }
            else
            {
                grupoMostrarResultado.SetActive(false);
                PreJogo();
            }
        }


        if (proximaRodada.tag.Equals("Perdeu"))
        {
            if (PlayerPrefs.GetInt("ptsMatematica") > PlayerPrefs.GetInt("pontuacaoMaximaMatematica"))
            {
                grupoMostrarResultado.SetActive(false);
                grupoHighScore.SetActive(true);
            }
            else
            {
                EncerrarPartida();
            }
        }
    }

    public void EncerrarPartida()
    {
        //grava highscore e nome vai para o pre jogo ou p/ menu principal
        PlayerPrefs.SetInt("nivelMatematica", 0);
        PlayerPrefs.SetInt("ptsMatematica", 0);
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void GravaPontuacaoMaxima()
    {
        PlayerPrefs.SetInt("pontuacaoMaximaMatematica", PlayerPrefs.GetInt("ptsMatematica"));
        PlayerPrefs.SetString("nomeVencedorMatematica", inputNomeVencedor.text);
        EncerrarPartida();
    }

    private int GetNivel()
    {
        return PlayerPrefs.GetInt("nivelMatematica");
    }

}
