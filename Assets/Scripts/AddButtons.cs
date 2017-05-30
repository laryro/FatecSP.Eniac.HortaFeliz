using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButtons : MonoBehaviour {

    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;


    public float width;

    private void Awake()
    {


   //     var gridLayoutGroup = GetComponent<GridLayoutGroup>();

//gridLayoutGroup.cellSize = new Vector2(parentRect.rect.width / cols, parentRect.rect.height / rows);


        for (int i = 0; i < 12; i++) {

            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);
        }


    }

}
