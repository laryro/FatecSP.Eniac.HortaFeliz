using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PtOneController : PtBaseController
{
    private List<Sprite> gamePuzzeles = new List<Sprite>();

    public Button proximaPuzzel, button;

    public Text TextDinamico;

    public Text txtResposta;

    [SerializeField]
    public InputField InputField;

    private PluralItem currentItem;

    private bool respostaRevelada = false;


    void OnGUI()
    {
        if (InputField.isFocused && (!string.IsNullOrEmpty(InputField.text)) && Input.GetKey(KeyCode.Return))
        {
            Jogar();
        }
    }


    void Start() {

        InicializaListaItens();

        Shuffle();

        IniciarJogadaGame();
        

        InputField.Select();
        InputField.ActivateInputField();


    }



    public void proximoJogo() {

        panelContent.SetActive(true);

        grupoMostrarResultado.SetActive(false);

        if(Itens.Any())
        IniciarJogadaGame();
        else
        {

            CarregaMensagemMundacaJogo();

            StartCoroutine(novaFase());

           
        }

    }


    IEnumerator novaFase()
    {
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("GamePortTwo");

    }

    public void IniciarJogadaGame()
    {
        InputField.Select();

        InputField.ActivateInputField();

        currentItem = Itens.FirstOrDefault() ?? null ;

        txtResposta.text = "Ver Resposta";

        respostaRevelada = false;

        if (currentItem != null)
        {
            button.image.sprite = currentItem.Imagem;

            TextDinamico.text = currentItem.Nome;
        }

    }

    public void Jogar() {

        numeroJogas++;

        if (currentItem.NomePlural.ToLower() == InputField.text.ToLower())
        {

            CarregaAcerto();

            if(!respostaRevelada)
            ComputarPontos(10 / numeroJogas);

            numeroJogas = 0;

            Itens.Remove(currentItem);

        }
        else {

            CarregaErro();

        }


        InputField.text = string.Empty;

        StartCoroutine(Tempo());

    }

    IEnumerator Tempo()
    {
        yield return new WaitForSeconds(2f);

        proximoJogo();

    }



    void Shuffle()
    {

        for (int i = 0; i < Itens.Count; i++)
        {
            PluralItem temp = Itens[i];
            int randomIndex = Random.Range(i, Itens.Count);
            Itens[i] = Itens[randomIndex];
            Itens[randomIndex] = temp;

        }

        Itens = Itens.Take(12).ToList();

    }

    public void MostrarResponta()
    {

        txtResposta.text = currentItem.NomePlural;
        respostaRevelada = true;

    }



}

