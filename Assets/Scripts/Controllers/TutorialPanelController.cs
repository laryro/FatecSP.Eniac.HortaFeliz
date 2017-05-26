using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanelController : MonoBehaviour
{

    private TutorialPanel painel;
    public Text textoDescritivo;

    #region Matematica
    public AudioClip somPapel, somPlastico, somMetal, somVidro, somOrganico,somDescoberta;
    public SpriteRenderer spritePapel, spriteVidro, spritePlastico, spriteMetal, spriteOrganico;
    public Text textoPapel, textoVidro, textoPlastico, textoMetal, textoOrganico;
    public GameObject botaoPapel, botaoVidro, botaoPlastico, botaoMetal, botaoOrganico;
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
                botaoMetal.SetActive(false); botaoOrganico.SetActive(false); botaoPlastico.SetActive(false);botaoPapel.SetActive(false); botaoVidro.SetActive(false);
                CarregarTutorialMatematica();
                break;
            case "Portugues":
                // Not developed yet.
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

    private void CarregarTutorialMatematica()
    {
        painel = new TutorialPanel();

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

            //Tutorial Vidro
            case 6:
                painel.TextoDescritivo = @"Você desbloqueou um novo item: Vidro.

O vidro é um dos produtos mais utilizados nas tarefas do dia-a-dia.
Ao ser descartado por pessoas e empresas, pode passar por um processo de reciclagem que garante seu reaproveitamento na producao do vidro reciclado.

Tipos de vidro recicláveis : 
Garrafas de sucos, refrigerantes, cervejas e outros tipos de bebidas;
Potes de alimentos, Cacos de vidros, Frascos de remédios
Preste atencao  no som do Vidro:
			";
                painel.MostraVidro = true;
                GetComponent<AudioSource>().PlayOneShot(somDescoberta);
                break;
            //Tutorial Metal
            case 11:
                painel.TextoDescritivo = @"Você desbloqueou um novo item: Metal.
O metal é um dos produtos mais utilizados nas tarefas do dia-a-dia. Encontramos embalagens de metais, fios e outros produtos metálicos em diversos produtos.

Tipos de metais recicláveis : Latas de alumínio (refrigerante, cerveja, etc.) e aco (latas de sardinha, molhos, óleo, etc.

<b>O intervalo entre os sons ficará menor. Fique atento.
Preste atencao  no som do Metal:</b>
			";
                painel.MostraMetal = true;
                GetComponent<AudioSource>().PlayOneShot(somDescoberta);
                break;
            case 20:
                painel.TextoDescritivo = @"Você desbloqueou um novo item: Lixo Orgânico.
Lixo orgânico é todo resíduo de origem vegetal ou animal, ou seja, todo lixo originário de um ser vivo. 
Este tipo de lixo é produzido nas residências, escolas, empresas e pela natureza.
 
Podemos citar como exemplos de lixo orgânico: restos de alimentos orgânicos (carnes, vegetais, frutos, cascas de ovos), papel, madeira, ossos, sementes, etc.
A Partir de agora o jogo ficará muito mais difícil! 
				Preste atencao no som do Lixo Orgânico:
			";
                GetComponent<AudioSource>().PlayOneShot(somDescoberta);
                painel.MostraOrganico = true;
                break;
            default:
                painel.TextoDescritivo = @"Soma Musical
Você ouvirá uma sequência de sons dos materiais recicláveis e cada um deles equivale a um número. 
Após ouvir toda a sequência, você deverá responder qual é o resultado da soma correspondente aos sons ouvidos.
Ex: Som de plastico = 1  , Som de papel = 3.
Ao ouvir uma sequência com os dois sons, você deverá colocar o resultado (1+3) = <b>4</b>
Aproveite esse momento e ouca bem cada um dos sons antes de comecar o jogo.
    Boa Sorte!
";
                painel.MostraMetal = painel.MostraOrganico = painel.MostraPapel = painel.MostraVidro = painel.MostraPlastico = true;
                break;

        }
        #endregion
        textoDescritivo.text = painel.TextoDescritivo;
        spriteMetal.enabled = textoMetal.enabled =  painel.MostraMetal;
        spriteOrganico.enabled = textoOrganico.enabled =  painel.MostraOrganico;
        spritePlastico.enabled = textoPlastico.enabled =  painel.MostraPlastico;
        spritePapel.enabled = textoPapel.enabled  = painel.MostraPapel;
        spriteVidro.enabled = textoVidro.enabled  = painel.MostraVidro;
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


}
