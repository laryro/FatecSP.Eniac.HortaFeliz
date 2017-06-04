using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantVaseController : MonoBehaviour, IDropHandler
{
    private PlantVase plantVase;

    public void Start()
    {
        plantVase = gameObject.GetComponent<PlantVase>();
        Debug.Log(plantVase);
    }
    public void OnDrop(PointerEventData eventData){
        //Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        plantVase.CheckNeeds();
        CareItem c = eventData.pointerDrag.GetComponent<CareItem>();
        if( c != null)
        {
            Debug.Log(c.fullfills);
        }
    }
}
