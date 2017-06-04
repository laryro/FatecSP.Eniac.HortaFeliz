using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PtOneController : PtBaseController
{
    private List<Sprite> gamePuzzeles = new List<Sprite>();

    public Button proximaPuzzel;

    private int numberGame = 12;

    public Text TextDinamico;

    [SerializeField]
    public InputField InputField;

    private PluralItem currentItem;



    void Start() {

        InicializaListaItens();

        Shuffle(Itens);

        IniciarJogadaGame();
    }



    public void proximoJogo() {


        panelContent.SetActive(true);

        grupoMostrarResultado.SetActive(false);

        if(Itens.Any())
        IniciarJogadaGame();
        else
        {
            SceneManager.LoadScene("GamePortTwo");

      //    StartCoroutine(Tempo());
        }

    }

    public void IniciarJogadaGame()
    {
        currentItem = Itens.FirstOrDefault();

        if (currentItem != null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("PuzzleButton");

            obj.GetComponent<Button>().image.sprite = currentItem.Imagem;

            TextDinamico.text = currentItem.Nome;
        }

    }

    public void Jogar() {

        numeroJogas++;

        if (currentItem.NomePlural.ToLower() == InputField.text.ToLower())
        {

            CarregaAcerto();

            ComputarPontos(10 / numeroJogas);

            numeroJogas = 0;

            Itens.Remove(currentItem);

        }
        else {

            CarregaErro();
        }

          InputField.text = string.Empty;

    }


    IEnumerator Tempo() {

        yield return new WaitForSeconds(1f);

   

    }


 


    void Shuffle(List<PluralItem> list)
    {

        for (int i = 0; i < list.Count; i++)
        {
            PluralItem temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;

        }
    }


}

