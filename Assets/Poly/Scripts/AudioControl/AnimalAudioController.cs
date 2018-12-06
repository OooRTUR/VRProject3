using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAudioController : MonoBehaviour {
    [SerializeField] AudioSource voiceSource;
    [SerializeField] AudioClip voiceClip;


	// Use this for initialization
	void Start () {
        voiceSource.clip = voiceClip;
	}
	
	// Update is called once per frame
	void Update () {
        if (!voiceSource.isPlaying)
            voiceSource.Play();
		
	}
}
