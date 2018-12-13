using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAudioControllerShort : MonoBehaviour {

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioClip[] dangerClips;
    [SerializeField] float delay = 4.0f;
    [SerializeField] bool isDanger;

    int index;
    int dangerIndex;

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
                yield return new WaitForSeconds(Random.Range(delay-0.5f,delay+0.5f));
                SwitchClips();
                if (isDanger)
                {
                    StopCoroutine("PlayClip");
                    StartCoroutine("PlayDangerClip");
                }
            }
            yield return new WaitForSeconds(Random.Range(0.9f,1.1f));
        }
    }

    IEnumerator PlayDangerClip()
    {
        Debug.Log("Starting danger clips");
        while (true)
        {
            if (!source.isPlaying)
            {
                SwitchDangerClips();
                
            }
            if (!isDanger)
            {
                StopCoroutine("PlayDangerClip");
                StartCoroutine("PlayClip");
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
        //Debug.Log("Запуск клипа");
        source.Play();
    }

    void SwitchDangerClips()
    {
        if (dangerClips.Length !=0)
        {
            dangerIndex++;
            if (dangerIndex >= dangerClips.Length)
                dangerIndex = 0;
            source.clip = clips[dangerIndex];
            //Debug.Log("Запуск клипа угрозы");
            source.Play();
        }
        else
        {
            source.Stop();
        }
    }
}
