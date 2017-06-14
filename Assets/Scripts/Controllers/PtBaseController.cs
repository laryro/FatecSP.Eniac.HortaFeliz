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

    const string mesagem = "Proximo Nivel \n Agora, brinque com o Jogo da Memoria.";

    const string mensagemFim = "Parabéns !!! \n Fim de Jogo Pontuação: ";

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

        Itens.Add(new PluralItem(11, "Ramo", "Ramos", Resources.Load<Sprite>("Ramo")));

        Itens.Add(new PluralItem(12, "Cebolinha", "Cebolinhas", Resources.Load<Sprite>("cebolinha")));

        Itens.Add(new PluralItem(13, "Alecrim", "Alecrins", Resources.Load<Sprite>("alecrim")));

        Itens.Add(new PluralItem(14, "Hortelã", "Hortelãs", Resources.Load<Sprite>("hortela")));

        Itens.Add(new PluralItem(15, "Folha", "Folhas", Resources.Load<Sprite>("hortela")));

        Itens.Add(new PluralItem(16, "Garrafa", "Garrafas", Resources.Load<Sprite>("outra garrafa")));

        Itens.Add(new PluralItem(17, "Feijão", "Feijões", Resources.Load<Sprite>("feijao")));

        Itens.Add(new PluralItem(18, "Agrião", "Agriões", Resources.Load<Sprite>("agriao")));

        Itens.Add(new PluralItem(19, "Broto", "Brotos", Resources.Load<Sprite>("broto_3")));

        Itens.Add(new PluralItem(20, "Raiz", "Raizes", Resources.Load<Sprite>("raiz")));

        Itens.Add(new PluralItem(21, "Lata", "Latas", Resources.Load<Sprite>("latinha")));

        Itens.Add(new PluralItem(22, "Papel", "Papéis", Resources.Load<Sprite>("papel")));

        Itens.Add(new PluralItem(23, "Girasol", "Girassóis", Resources.Load<Sprite>("sunflower")));

        Itens.Add(new PluralItem(24, "Couve-flor", "Couves-Flores", Resources.Load<Sprite>("couveflor")));






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


    public void CarregaMensagemMundacaJogo() {

        panelContent.SetActive(false);
        grupoMostrarResultado.SetActive(true);
        mostraResultadoPainel.color = new Color(0.16f, 0.77f, 0.16f);
        txtMensagemResultado.text = mesagem;

    }

    public void CarregaFimGame() {

        panelContent.SetActive(false);
        grupoMostrarResultado.SetActive(true);
        mostraResultadoPainel.color = new Color(0.16f, 0.77f, 0.16f);
        txtMensagemResultado.text = mensagemFim + PlayerPrefs.GetInt("ptsPortugues").ToString();

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