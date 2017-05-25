using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {





    
    // Use this for initialization
    void Start () {

        if (this.name.Equals("MusicaFundo")){
            print("Audio Iniciado e permanecera em loop infinito");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void Awake()
    {
        DontDestroyOnLoad(this.transform.gameObject);
    }
}
