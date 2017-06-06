using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Need : MonoBehaviour{
	public Seed.Needs type;

	public void SetSprite( Sprite sprite ) {
		this.GetComponent<Image>().sprite = sprite;
	}
}