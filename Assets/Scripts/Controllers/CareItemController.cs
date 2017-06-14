using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CareItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject draggableShadow = null;

    public void Start()
    {
        draggableShadow = gameObject.transform.GetChild(0).gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        draggableShadow.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        draggableShadow.transform.position = gameObject.transform.position;
    }
}
