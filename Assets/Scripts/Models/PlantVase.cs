using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantVase : PlantVaseController {
    private Seed seed;
    private GameObject needsPanel;

	public List<GameObject> needsList;

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
			GameObject nu = (GameObject) Instantiate(needsList[i], needsPanel.transform.position, needsPanel.transform.rotation);

			nu.GetComponent<Need>().SetSprite(Resources.Load<Sprite>("Science/NEED_WATER") );
			nu.transform.SetParent( needsPanel.transform );

        }
		Debug.Log( needsPanel.transform.childCount );

    }
}
