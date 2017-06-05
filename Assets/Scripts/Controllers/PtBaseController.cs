using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PtBaseController : MonoBehaviour
{
    public List<PluralItem> Itens = new List<PluralItem>();

    public AudioClip somAcerto, somErro;

    public Text txtMensagemResultado;

    public Text txtPontuacao, txtRecordista;

    public Image mostraResultadoPainel;

    public GameObject grupoMostrarResultado, grupoHighScore;

    public GameObject panelContent;

    public InputField inputNomeVencedor;


    public int numeroJogas = 0;


    const string mensagemErro = "Você Errou :( \n Tente Novamente.";

    const string mensagemAcerto = "Parabéns !!!! \n Você Acertou !!!!";

    public void InicializaListaItens()
    {
        Itens.Add(new PluralItem(1, "Semente", "Sementes", Resources.Load<Sprite>("semente")));

        Itens.Add(new PluralItem(2, "Joaninha", "Joaninhas", Resources.Load<Sprite>("joaninha")));

        Itens.Add(new PluralItem(3, "Sol", "Sóis", Resources.Load<Sprite>("sol")));

        Itens.Add(new PluralItem(4, "Regador", "Regadores", Resources.Load<Sprite>("regador")));

        Itens.Add(new PluralItem(5, "Adubo", "Adubos", Resources.Load<Sprite>("adubo")));

        Itens.Add(new PluralItem(6, "Terra", "Terras", Resources.Load<Sprite>("terra")));

        Itens.Add(new PluralItem(7, "Água", "Águas", Resources.Load<Sprite>("agua")));

        Itens.Add(new PluralItem(8, "Tesoura", "Tesouras", Resources.Load<Sprite>("tesoura")));

        Itens.Add(new PluralItem(9, "Vaso", "Vasos", Resources.Load<Sprite>("vaso")));

        Itens.Add(new PluralItem(10, "Pá", "Pás", Resources.Load<Sprite>("pa")));

        txtRecordista.text = "Recordista: " + PlayerPrefs.GetString("nomeVencedorPortugues") + " : " + PlayerPrefs.GetInt("pontuacaoMaximaPortugues") + " PTS";

    }

    void OnDestroy()
    {
        PlayerPrefs.Save();
    }

    public void CarregaAcerto(bool painel = false, string sprite = null)
    {
        if(!painel)
       panelContent.SetActive(false);

        grupoMostrarResultado.SetActive(true);
        mostraResultadoPainel.color = new Color(0.16f, 0.77f, 0.16f);

        if (sprite != null)
        {
            var item = Itens.FirstOrDefault(x => x.Imagem.name.ToLower() == sprite.ToLower());

            if (item != null) {

                string texto = string.Format("Parabéns !!!! \n  O Plural de {0} é : {1}", item.Nome, item.NomePlural);

                txtMensagemResultado.text = texto;

            }
        }
        else {

            txtMensagemResultado.text = mensagemAcerto;
        }

        GetComponent<AudioSource>().PlayOneShot(somAcerto);
    }

    public void CarregaErro(bool painel = false)
    {
        if (!painel)
            panelContent.SetActive(false);

        grupoMostrarResultado.SetActive(true);
        mostraResultadoPainel.color = new Color(1f, 0.5f, 0.5f);
        txtMensagemResultado.text = mensagemErro;
        GetComponent<AudioSource>().PlayOneShot(somErro);

    }

    public void Shuffle(List<Sprite> list)
    {

        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;

        }
    }

    public void ComputarPontos(int valor ) {

        PlayerPrefs.SetInt("ptsPortugues", PlayerPrefs.GetInt("ptsPortugues") + valor);

        txtPontuacao.text = PlayerPrefs.GetInt("ptsPortugues").ToString();

    }


    public void EncerrarPartida()
    {
        //grava highscore e nome vai para o pre jogo ou p/ menu principal
        PlayerPrefs.SetInt("ptsPortugues", 0);
        SceneManager.LoadScene("MainMenu");
    }

    public void GravaPontuacaoMaxima()
    {
        PlayerPrefs.SetInt("pontuacaoMaximaPortugues", PlayerPrefs.GetInt("ptsPortugues"));
        PlayerPrefs.SetString("nomeVencedorPortugues", inputNomeVencedor.text);
        EncerrarPartida();
    }


}