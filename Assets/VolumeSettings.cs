using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour {

    public AudioMixer mixer;
    [SerializeField] float volume;

	// Use this for initialization
	void Start () {
        mixer.SetFloat("Volume", 60.0f);
	}

    public void SetFxLvlDown()
    {
        if (volume > -50)
        {
            volume -= 10;
            mixer.SetFloat("Volume", volume);
        }
    }

    public void SetFxLvlUp()
    {
        if (volume < 0)
        {
            volume += 10;
            mixer.SetFloat("Volume", volume);
        }
    }


    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 150, 100), "Down"))
    //    {
    //        SetFxLvlDown();
    //    }

    //    if (GUI.Button(new Rect(10, 120, 150, 100), "Up"))
    //    {
    //        SetFxLvlUp();
    //    }
    //}
}
