using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantVase : PlantVaseController {
    private Seed seed;
	private GameObject needsPanel, plantStage, score;
    private bool rightNeed, careable;
    private int errorMod = 1;

	private AudioSource audioPlayer;

	public AudioClip clearAudio, scoreAudio, errorAudio;
	public List<Sprite> plantStages;
    public List<GameObject> needsList;

    public new void Start(){
        base.Start();
        careable = true;
		audioPlayer = this.GetComponent<AudioSource>();

        needsPanel = gameObject.transform.Find("NeedsList").gameObject;
        plantStage = gameObject.transform.Find("Plant").gameObject;
        plantStage.GetComponent<Image>().color = new Color(1f,1f,1f,0f);

        score = GameObject.Find("ScoreBlock").gameObject;

        seed = new Seed();
        ShowNeeds();
    }

    public void CheckNeeds (Seed.Needs care)
	{
		rightNeed = false;

		if (careable) {
			for (int i = 0; i < needsList.ToArray ().Length; i++) {
				Need need = needsPanel.transform.GetChild (i).gameObject.GetComponent<Need> ();
				if (need.type == care && !need.IsFullfilled ()) {
					need.SetFullfilled ();
					rightNeed = true;
					// Soma pontos
					score.GetComponent<ScoreController> ().AddPoints (10);
					plantStage.GetComponent<Image> ().sprite = plantStages [ 3 - RemainingNeeds()];
					plantStage.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
					errorMod = 1;
					break;
				}
			}
			// Se for o cuidado errado
			if (!rightNeed) {
				// Desconta pontos
				score.GetComponent<ScoreController> ().SubtractPoints (errorMod);
				errorMod++;
				audioPlayer.PlayOneShot (errorAudio);
			}

			// Checa se todos as necessidades foram preenchidas, remove e adiciona outro vaso no lugar
			if (RemainingNeeds() == 0) {
				score.GetComponent<ScoreController> ().AddPoints (100);
				audioPlayer.PlayOneShot (clearAudio);
				careable = false;
				StartCoroutine( WaitUpAndDeactivate(3));
				StartCoroutine (WaitUpAndRenew(5));
			} else if (rightNeed) {
				audioPlayer.PlayOneShot (scoreAudio);
			}
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
        Vector2 needsSize = needsPanel.GetComponent<RectTransform>().sizeDelta;
        needsSize.x = (Screen.width / needsSize.x) * 2.7f;
        needsSize.y = needsSize.x;
        
        for (int i = 0; i < needs.Length; i++)
        {
			GameObject nu = (GameObject) Instantiate(needsList[i], needsPanel.transform.position, needsPanel.transform.rotation);

            needsList[i].GetComponent<Need>().type = needs[i];
            nu.GetComponent<Need>().type = needs[i];
            nu.GetComponent<Need>().SetSprite(Resources.Load<Sprite>("Science/" + needs[i].ToString()) );
            nu.GetComponent<RectTransform>().sizeDelta = needsSize;
            nu.transform.SetParent( needsPanel.transform );
        }
    }

    private void RenewVase()
    {
        for( int i = needsPanel.transform.childCount - 1; i >= 0 ; i--){
            Destroy(needsPanel.transform.GetChild(i).gameObject);
        }

		this.GetComponent<Image>().color = new Color(1f,1f,1f,1f);
		plantStage.GetComponent<Image>().color = new Color (1f, 1f, 1f, 0f);

        seed = new Seed();
        ShowNeeds();
    }

    private IEnumerator WaitUpAndDeactivate (int time){
    	yield return new WaitForSeconds(time);
    	this.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.3f);
		plantStage.GetComponent<Image>().color = new Color( 1f,1f,1f,0.3f);
	}
	private IEnumerator WaitUpAndRenew(int time){
		yield return new WaitForSeconds(time);
		RenewVase();
		careable = true;
	}
		
}
