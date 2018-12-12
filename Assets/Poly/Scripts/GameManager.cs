using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField] GameObject gameMenu;
    [SerializeField] Image fader;
    [SerializeField] bool isRestarted = false;

	// Use this for initialization
	void Start () {
        Debug.Log("scene loaded");
        SceneManager.sceneLoaded += OnSceneLoaded;
        Fader(0);
    }
    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRestarted = true;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(gameMenu);
            SceneManager.LoadScene(0);
        }
    }

    public void Fader (float toFade)
    {
        StopCoroutine("Fading");
        StartCoroutine(Fading(toFade));
    }

    IEnumerator Fading (float toFade)
    {
        Color alpha = fader.color;
        float difference = 1;
        alpha.a = toFade;
        while (difference > 0.001f)
        {
            fader.color =  Color.Lerp(fader.color, alpha, 0.1f);
            difference = Mathf.Abs(fader.color.a - alpha.a);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
    }
}
