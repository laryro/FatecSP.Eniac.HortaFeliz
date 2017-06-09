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
    }
    public void OnDrop(PointerEventData eventData){
        CareItem c = eventData.pointerDrag.GetComponent<CareItem>();
        if( c != null)
        {
            plantVase.CheckNeeds(c.fullfills);
        }
    }
}
