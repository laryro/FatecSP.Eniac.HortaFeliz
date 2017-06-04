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
        needsPanel = gameObject.transform.GetChild(0).gameObject;
        ShowNeeds();
    }

    public void Update()
    {
        
    }

    public void CheckNeeds()
    {
        Debug.Log("Checando necessidades");
    }
    private void ShowNeeds()
    {
        Debug.Log("Adicionando necessidades no painel " + needsPanel.name);
        Seed.Needs[] needs = seed.GetNeeds().ToArray();
        for (int i = 0; i < needs.Length; i++)
        {
            
        }
    }
}
