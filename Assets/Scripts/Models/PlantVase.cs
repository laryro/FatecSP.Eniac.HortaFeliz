using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantVase : PlantVaseController {
    private Seed seed;
    private GameObject needsPanel;
    private bool rightNeed;

	public List<GameObject> needsList;

    public new void Start(){
        base.Start();
        needsPanel = gameObject.transform.Find("NeedsList").gameObject;

        seed = new Seed();
        ShowNeeds();
    }

    public void CheckNeeds(Seed.Needs care){
        rightNeed = false;

        for( int i = 0; i < needsList.ToArray().Length; i++){
            Need need = needsPanel.transform.GetChild(i).gameObject.GetComponent<Need>();
            if ( need.type == care && !need.IsFullfilled() ){
                need.SetFullfilled();
                rightNeed = true;
                // Soma pontos
                break;
            }
        }
        // Se for o cuidado errado
        if( !rightNeed)
        {
            // Desconta pontos
            Debug.Log("Errou, vai descontar pontos.");
        }

        // Checa se todos as necessidades foram preenchidas, remove e adiciona outro vaso no lugar
        if ( RemainingNeeds() == 0)
        {
            RenewVase();
        }
    }

    /**
     * Faz a contagem de cuidados ainda não tomados.
     */
    private int RemainingNeeds()
    {
        int nonFullfilledNeeds = 0;
        for(int i = 0; i < needsList.ToArray().Length; i++)
        {
            if (!needsPanel.transform.GetChild(i).gameObject.GetComponent<Need>().IsFullfilled()) nonFullfilledNeeds++;
        }
        return nonFullfilledNeeds;
    }
    private void ShowNeeds(){
        Seed.Needs[] needs = seed.GetNeeds().ToArray();
        for (int i = 0; i < needs.Length; i++)
        {
			GameObject nu = (GameObject) Instantiate(needsList[i], needsPanel.transform.position, needsPanel.transform.rotation);

            needsList[i].GetComponent<Need>().type = needs[i];
            nu.GetComponent<Need>().type = needs[i];
            nu.GetComponent<Need>().SetSprite(Resources.Load<Sprite>("Science/" + needs[i].ToString()) );
			nu.transform.SetParent( needsPanel.transform );
        }
    }

    private void RenewVase()
    {
        Debug.Log("Renovando o vaso, já que ele foi preenchido")
        for( int i = needsPanel.transform.childCount - 1; i >= 0 ; i--){
            //needsPanel.transform.GetChild(i).gameObject.
        }

        //seed = new Seed();
        //ShowNeeds();
    }
}
