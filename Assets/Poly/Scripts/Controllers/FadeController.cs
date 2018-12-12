using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {

    Image fader;

    private void Awake()
    {
        fader = GetComponent<Image>();
    }

    private void Start()
    {
        Fader(0);
    }

    public void Fader(float toFade)
    {
        StopCoroutine("Fading");
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
        yield return new WaitForSeconds(1.5f);
    }
}
