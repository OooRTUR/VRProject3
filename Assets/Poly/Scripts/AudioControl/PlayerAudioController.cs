using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour {

    [SerializeField] AudioSource legSource;
    [SerializeField] AudioSource breathSource;

    [SerializeField]AudioClip runClip;
    [SerializeField] AudioClip breathClip;

    
	// Use this for initialization
	void Start () {
        legSource.clip = runClip;
        breathSource.clip = breathClip;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            if(!legSource.isPlaying)
                legSource.Play();
            if (!breathSource.isPlaying)
                breathSource.Play();
        }
        else
        {
            if(legSource.isPlaying)
                legSource.Stop();
            if (breathSource.isPlaying)
                breathSource.Play();
        }
    }
}
