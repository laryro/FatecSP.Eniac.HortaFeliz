using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantVase : PlantVaseController {
    private Seed seed;
    private GameObject needsPanel;
    private List<GameObject> needsList;

    public new void Start()
    {
        base.Start();
        seed = new Seed();
        needsPanel = gameObject.transform.Find("NeedsList").gameObject;
        ShowNeeds();
    }

    public void Update()
    {
        
    }

    public void CheckNeeds()
    {
        
    }
    private void ShowNeeds()
    {
        Debug.Log("Adicionando necessidades no painel " + needsPanel.name);
        Seed.Needs[] needs = seed.GetNeeds().ToArray();
        for (int i = 0; i < needs.Length; i++)
        {
			Need needObject = this.gameObject.AddComponent<Need>() as Need;
			needObject.Sprite = Resources.Load("Images/Colorful/gota") as Sprite;

			Debug.Log (needObject.Sprite);

//			Need needObject =  Need ();
//			needObject.type = needs [i];
//			needObject.Sprite = Resources.Load("Images/Colorful/gota") as Sprite;
//			Debug.Log (needObject.Sprite );
//
//			GameObject need = (GameObject)Instantiate (needObject, transform.position, transform.rotation);
//			Debug.Log (need);
//			Need need = Instantiate (needObject, transform.position, transform.rotation);
//			Debug.Log (need.transform);
        }
    }
}
