using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAudioControllerShort : MonoBehaviour {

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] clips;

    int index;

	void Start () {
        index = 0;
        source.clip = clips[index];
        StartCoroutine("PlayClip");
	}
	
	IEnumerator PlayClip()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        while (true)
        {
            if (!source.isPlaying)
            {
                yield return new WaitForSeconds(4.0f);
                SwitchClips();
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    void SwitchClips()
    {
        index++;
        if (index >= clips.Length)
            index = 0;
        source.clip = clips[index];
        Debug.Log("Запуск клипа");
        source.Play();
    }
}
