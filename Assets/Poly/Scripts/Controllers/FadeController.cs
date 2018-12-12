using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour {

    public static FadeController instance;

    Image fader;

    private void Awake()
    {
        instance = this;
        fader = GetComponent<Image>();
    }

    private void Start()
    {
        Fader(0);
    }

    public void Fader(float toFade)
    {
        StartCoroutine(Fading(toFade));
    }

    IEnumerator Fading(float toFade)
    {
        Color alpha = fader.color;
        float difference = 1;
        alpha.a = toFade;
        while (difference > 0.001f)
        {
            fader.color = Color.Lerp(fader.color, alpha, 0.1f);
            difference = Mathf.Abs(fader.color.a - alpha.a);
            yield return null;
        }
        if (toFade == 1)
            SceneManager.LoadScene(0);
    }
}
