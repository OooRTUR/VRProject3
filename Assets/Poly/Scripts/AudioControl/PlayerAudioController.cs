using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour {

    [SerializeField] [Range(0.0f,1.0f)] float musicVolume;
    [SerializeField] [Range(0.0f,1.0f)] float ambientVolume;

    [SerializeField] FieldOfViewAudio fow;

    [SerializeField] AudioSource legSource;
    [SerializeField] AudioSource breathSource;
    [SerializeField] AudioSource ambientSource;
    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioClip runClip;
    [SerializeField] AudioClip breathClip;
    [SerializeField] AudioClip[] musicClips;
    [SerializeField] AudioClip[] blizzardClips;
    [SerializeField] AudioClip[] dangerClips;

    [SerializeField] public bool isDanger = false;

    int musicIndex = -1;
    int blizzardIndex = -1;
    int dangerIndex = -1;

    int dangerSignalsCount = 0;

    // Use this for initialization
    void Start () {
        legSource.clip = runClip;
        breathSource.clip = breathClip;
        ambientSource.clip = blizzardClips[0];
        musicSource.clip = musicClips[0];
        StartCoroutine("SwitchMusic");
        StartCoroutine("SwitchAmbient");
	}

    float dangerTime;
	void Update () {
        musicSource.volume = musicVolume;
        ambientSource.volume = ambientVolume;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
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

    IEnumerator SwitchMusic()
    {
        musicVolume = 0.22f;
        yield return new WaitForSeconds(4.0f); // одиночное ожидание на старте игры
        while (true)
        {
            Debug.Log("Смена пластинки");
            SwitchMusicClips();
            while (musicSource.isPlaying)
            {
                if (fow.isDanger)
                    break;
                yield return new WaitForSeconds(1.0f); //проверка каждую секунду
            }
            if (fow.isDanger)
                break;
           
            yield return new WaitForSeconds(10.0f); // пауза между клипами
        }
        Debug.Log("Угроза");
        StopCoroutine("SwitchMusic");
        StartCoroutine("SwitchDanger");

    }
    IEnumerator SwitchDanger()
    {
        musicVolume = 1.0f;
        dangerIndex = -1;
        while (true)
        {
            Debug.Log("Пластинка угрозы");
            SwitchDangerClips();
            while(musicSource.isPlaying)
            {
                if (!fow.isDanger)
                    break;
                yield return new WaitForSeconds(0.1f);
            }
            if (!fow.isDanger)
                break;
            yield return null;
        }
        Debug.Log("нет угрозы");
        StopCoroutine("SwitchDanger");
        StartCoroutine("SwitchMusic");
    }
    IEnumerator SwitchAmbient()
    {
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            Debug.Log("Смена эмбиента");
            SwitchAmbientClips();
            while (ambientSource.isPlaying)
            {
                yield return new WaitForSeconds(1.0f);
            }
            yield return null;
        }
    }
    IEnumerator DangerTimer()
    {
        dangerTime = 0.0f;
        while (true)
        {
            dangerTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void SendDangerSignal()
    {
        dangerSignalsCount++;
    }

    
    void SwitchMusicClips()
    {
        musicIndex++;
        if (musicIndex >= musicClips.Length)
            musicIndex = 0;
        musicSource.clip = musicClips[musicIndex];
        musicSource.Play();
    }

    
    void SwitchAmbientClips()
    {
        blizzardIndex++;
        if (blizzardIndex >= blizzardClips.Length)
            blizzardIndex = 0;
        ambientSource.clip = blizzardClips[blizzardIndex];
        ambientSource.Play();
    }

    
    void SwitchDangerClips()
    {
        dangerIndex++;
        if (dangerIndex >= dangerClips.Length)
            dangerIndex = 0;
        musicSource.clip = dangerClips[dangerIndex];
        musicSource.Play();
    }
}
