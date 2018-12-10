using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour {

    [SerializeField] AudioSource legSource;
    [SerializeField] AudioSource breathSource;
    [SerializeField] AudioSource ambientSource;
    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioClip runClip;
    [SerializeField] AudioClip breathClip;
    [SerializeField] AudioClip[] musicClips;
    [SerializeField] AudioClip[] blizzardClips;

    
	// Use this for initialization
	void Start () {
        legSource.clip = runClip;
        breathSource.clip = breathClip;
        ambientSource.clip = blizzardClips[0];
        musicSource.clip = musicClips[0];
	}
	
	// Update is called once per frame
	void Update () {
        if (!ambientSource.isPlaying)
            ambientSource.Play();
        if (!musicSource.isPlaying)
            musicSource.Play();

        SwitchClips();

        if (Input.GetKey(KeyCode.W))
        {
            if(!legSource.isPlaying)
                legSource.Play();
            //if (!breathSource.isPlaying)
            //    breathSource.Play();
        }
        else
        {
            if(legSource.isPlaying)
                legSource.Stop();
            //if (breathSource.isPlaying)
            //    breathSource.Stop();
        }
    }

    int musicIndex = 0;
    void SwitchClips()
    {
        if(musicSource.time >= musicSource.clip.length-0.5f)
        {
            musicIndex++;
            if (musicIndex >= musicClips.Length)
                musicIndex = 0;
            musicSource.clip = musicClips[musicIndex];
        }
    }
}
//[System.Serializable]
//public struct MusicClips
//{
//    public AudioClip score;
//    public AudioClip score1;
//    public AudioClip score2;
//}
//[System.Serializable]
//public struct AmbientClips
//{
//    public AudioClip softBlizzard;
//    public AudioClip hardBlizzard;
//}
