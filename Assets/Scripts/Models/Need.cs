using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Need : MonoBehaviour{
	public Seed.Needs type;
    private bool fullfilled = false;

	public void SetSprite( Sprite sprite ) {
		this.GetComponent<Image>().sprite = sprite;
	}
    public bool IsFullfilled()
    {
        return fullfilled;
    }
    public void SetFullfilled()
    {
        fullfilled = true;
        this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
    }
}