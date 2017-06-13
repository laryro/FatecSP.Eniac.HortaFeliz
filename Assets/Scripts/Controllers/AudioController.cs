using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioClip otherClip;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

     void Update()
    {
        if (!audio.isPlaying)
        {
            audio.clip = otherClip;
            audio.Play();
        }
    }

    void Awake()
    {

        DontDestroyOnLoad(this.transform.gameObject);

        if (FindObjectsOfType(this.GetType()).Length > 1)
        {
            Destroy(this.transform.gameObject);
        }
    }


    /*
        public AudioClip audio;


        // Use this for initialization
        void Start () {

            if (this.name.Equals("MusicaFundo")){
                print("Audio Iniciado e permanecera em loop infinito");
            }
        }

        // Update is called once per frame
        void Update () {

        }


        */
}
