using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MathLevelController : MonoBehaviour
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

    private MathLevel matematica;
    private const int VLR_PAPEL = 3;
    private const int VLR_PLASTICO = 1;
    private const int VLR_VIDRO = 2;
    private const int VLR_METAL = 4;
    private const int VLR_ORGANICO = 9;
    private const int MULTIPLICADOR_PONTOS = 50;
    private bool enterUnlocked = true;
    private List<RecyclableItem> listaItens;
    private List<Int32> listaSelecionados;
    public InputField inputResultado, inputNomeVencedor;

    // Use this for initialization
    void Start()
    {
        //Instancia uma fase do jogo de matematica de acordo com o Level do Jogador
        matematica = new MathLevel(PlayerPrefs.GetInt("nivelMatematica"));
        listaItens = new List<RecyclableItem>(5);
        InicializaListaItens();
        PreJogo();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {

            if (grupoPreJogo.activeSelf == true && enterUnlocked == true)
            {
                TocarSequencia();
                enterUnlocked = false;
                Invoke("UnlockEnterKey", 1.0f);
            }



            if (grupoMostrarResultado.activeSelf == true && enterUnlocked == true)
            {
                NovaRodada();
                enterUnlocked = false;
                Invoke("UnlockEnterKey", 1.0f);
            }

        }
    }

    void OnDestroy()
    {
        PlayerPrefs.Save();
    }

    private void InicializaListaItens()
    {
        listaItens.Add(new RecyclableItem("Papel", VLR_PAPEL, rodapePapel, cestaPapel, containerPapel, txtQtdPapel));
        listaItens.Add(new RecyclableItem("Plastico", VLR_PLASTICO, rodapePlastico, cestaPlastico, containerPlastico, txtQtdPlastico));
        listaItens.Add(new RecyclableItem("Vidro", VLR_VIDRO, rodapeVidro, cestaVidro, containerVidro, txtQtdVidro));
        listaItens.Add(new RecyclableItem("Metal", VLR_METAL, rodapeMetal, cestaMetal, containerMetal, txtQtdMetal));
        listaItens.Add(new RecyclableItem("Organico", VLR_ORGANICO, rodapeOrganico, cestaOrganico, containerOrganico, txtQtdOrganico));
    }


    void OnGUI()
    {
        if (inputResultado.isFocused && (!string.IsNullOrEmpty(inputResultado.text)) && Input.GetKey(KeyCode.Return))
        {
            MostrarResultado();
        }
        /*
        if (proximaRodada. && Input.GetKey(KeyCode.Return))
        {
            MostrarResultado();
        }*/


    }

    /// <summary>
    /// Tela inical do jogo
    /// </summary>
    public void PreJogo()
    {
        //carrega botao de play
        grupoPreJogo.SetActive(true);

        //AtualizaPontuacao
        txtPontuacao.text = PlayerPrefs.GetInt("ptsMatematica").ToString();
        txtRecordista.text = "Recordista: " + PlayerPrefs.GetString("nomeVencedorMatematica") + " : " + PlayerPrefs.GetInt("pontuacaoMaximaMatematica") + " PTS";

        //Zera a sequencia dos itens sorteados
        foreach (var item in listaItens)
        {
            item.OcorrenciasItemRodada = 0;
            item.CestaLixo.enabled = false;
            item.ContainerItem.SetActive(false);
        }

        //Acrescenta dificuldade ao jogo em função do nivel
        matematica.AtribuirPorFase(GetNivel());
    }

    /// <summary>
    /// Funcao chamada pelo botao de play
    /// </summary>
    public void TocarSequencia()
    {
        //desliga interface pre-jogo e carrega interface da sequencia
        grupoPreJogo.SetActive(false);
        grupoTocarSequencia.SetActive(true);
        GerarSequencia();
    }

    /// <summary>
    /// Lógica estilo Genius
    /// </summary>
    public void GerarSequencia()
    {
        //Inicializa sequencia
        int resultSoma = 0;
        //lista sequencial dos itens sorteados
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
        //Resultado da operacao
        matematica.ResultadoSoma = resultSoma;

        for (int i = 0; i < qtdItemSequencia; i++)
        {
            //Apos carregar a lista dos selecionados
            //Chama a função que toca o som / pisca o som / pisca o item
            Invoke("TocaSom", 1 + (i * matematica.GetIntervaloSons()));
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

    /// <summary>
    /// Toca sempre o primeiro som da lista e remove o mesmo.
    /// No final da lista chama a funcao de resposta
    /// </summary>
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
        //Retira o item tocado, da fila
        listaSelecionados.RemoveAt(0);


        if (listaSelecionados.Count == 0)
        {
            //Aguarda um segundo para mostra a mensagem
            Invoke("AguardarResposta", 1.5f);
        }

    }

    /// <summary>
    /// Mostra Input de texto para interacaos
    /// </summary>
    public void AguardarResposta()
    {
        grupoTocarSequencia.SetActive(false);
        grupoAguardarResposta.SetActive(true);
        EventSystem.current.SetSelectedGameObject(inputResultado.gameObject, null);
    }

    /// <summary>
    /// Obtem resposta do jogador e mostra o resultado.
    /// Atribui pontucao
    /// </summary>
    public void MostrarResultado()
    {
        grupoAguardarResposta.SetActive(false);
        grupoMostrarResultado.SetActive(true);

        int resultadoJogador = 0;
        Int32.TryParse(inputResultado.text, out resultadoJogador);
        inputResultado.text = string.Empty;

        //Composicao do texto do resultado
        #region MensagemResultado
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
        #endregion

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
        enterUnlocked = false;
        Invoke("UnlockEnterKey", 1.0f);
        GetComponent<AudioSource>().PlayOneShot(somAcerto);
        PlayerPrefs.SetInt("nivelMatematica", (PlayerPrefs.GetInt("nivelMatematica") + 1));
    }

    public void CarregaErro()
    {
        mostraResultadoPainel.color = new Color(1f, 0.5f, 0.5f);
        GetComponent<AudioSource>().PlayOneShot(somErro);
        enterUnlocked = false;
        Invoke("UnlockEnterKey", 1.0f);
        proximaRodada.tag = "Perdeu";
        txtBtnProximaRodada.text = "Encerrar";
    }

    /// <summary>
    /// Determina a proxima rodada em funcao do resultado 
    /// </summary>
    public void NovaRodada()
    {
        if (proximaRodada.tag.Equals("Ganhou"))
        {
            if (GetNivel() == 6 || GetNivel() == 11 || GetNivel() == 21)
            {
                SceneManager.LoadScene("TutorialMath");
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

    /// <summary>
    /// Zera a pontuacao e volta para o menu principal
    /// </summary>
    public void EncerrarPartida()
    {
        //grava highscore e nome vai para o pre jogo ou p/ menu principal
        PlayerPrefs.SetInt("nivelMatematica", 0);
        PlayerPrefs.SetInt("ptsMatematica", 0);
        SceneManager.LoadScene("MainMenu");
    }

    public void GravaPontuacaoMaxima()
    {
        PlayerPrefs.SetInt("pontuacaoMaximaMatematica", PlayerPrefs.GetInt("ptsMatematica"));
        PlayerPrefs.SetString("nomeVencedorMatematica", inputNomeVencedor.text);
        EncerrarPartida();
    }

    /// <summary>
    /// Nivel do jogo geral
    /// </summary>
    /// <returns></returns>
    private int GetNivel()
    {
        return PlayerPrefs.GetInt("nivelMatematica");
    }



    public void UnlockEnterKey()
    {
        enterUnlocked = true;
    }
}
