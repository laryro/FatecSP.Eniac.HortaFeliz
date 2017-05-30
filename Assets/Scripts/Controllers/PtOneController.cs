using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PtOneController : MonoBehaviour {


    public List<PluralItem> Itens;

    // Elementos par Mudar

    public Text TextDinamico;

    public Image ImagemPlural;

    public InputField InputField;

    public PluralItem Item;



    // Use this for initialization
    void Start () {

        InicializaListaItens();


     //   myImageComponent = GetComponent<Image>();

        StarGame();

    }
	
	// Update is called once per frame
	void Update () {
		


	}


    private void InicializaListaItens()
    {

        Itens = new List<PluralItem>();

        Itens.Add(new PluralItem(1, "Alecrim", "Alecrins", ""));

        Itens.Add(new PluralItem(2, "Cebolinha", "Cebolinhas", ""));

        Itens.Add(new PluralItem(3, "Louro", "Louros", ""));

        Itens.Add(new PluralItem(4, "Girassol", "Girassóis", ""));

        Itens.Add(new PluralItem(5, "Joaninha", "Joaninhas", ""));

        Itens.Add(new PluralItem(6, "Folha", "Folhas", ""));

    }


    public void StarGame()
    {

        int id = Random.Range(1, Itens.Count);

        Debug.Log(id);


        Item = Itens.Find(x => x.Id == id);

      

        if (Item != null)
        {
            var texture = Resources.Load<Sprite>("joaninha");

            ImagemPlural.sprite = texture;

            TextDinamico.text = Item.Nome;

            InputField.text = Item.NomePlural;
        }




    }

}
