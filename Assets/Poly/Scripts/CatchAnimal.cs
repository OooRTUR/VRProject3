using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchAnimal : MonoBehaviour {

	[Header("UI")]
	public RectTransform catchTextTrans;
	public Image fillImage;
    public Renderer rend;
    BoxCollider col;

	float catchTimer;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    public void Catch (string tag) {
		catchTimer += Time.deltaTime;
		fillImage.fillAmount = catchTimer / 2.5f;
		if (fillImage.fillAmount == 1) {
			ResetFill ();
            UpdateHunt(tag);
            StartCoroutine("MoveCatchText");
            rend.enabled = false;
            col.enabled = false;
		}
    }

    public void ResetFill()
    {
        fillImage.fillAmount = 0;
        catchTimer = 0;
    }

	IEnumerator MoveCatchText () {
		catchTextTrans.anchoredPosition = Vector3.zero;
		Vector3 movement = new Vector3 (0.01f, 0.01f, 0);
		float timer = 0;
		Text text = catchTextTrans.GetComponent<Text> ();
		Color alpha = text.color;
		alpha.a = 1;
		catchTextTrans.gameObject.SetActive (true);
		while (true) {
			timer += Time.deltaTime;
			alpha.a -= Time.deltaTime * 0.5f;
			text.color = alpha;
			catchTextTrans.Translate (movement);
			if (timer > 2)
				break;
			yield return null;
		}
		catchTextTrans.gameObject.SetActive (false);
        gameObject.SetActive(false);
	}

    void UpdateHunt (string tag)
    {
        switch(tag)
        {
            case "Mouse":
                GameMenu.instance.mouses++;
                break;
            case "Rabbit":
                GameMenu.instance.rabbits++;
                break;
            case "Chicken":
                GameMenu.instance.chickens++;
                break;
            case "Hole":
                GameMenu.instance.mouses++;
                break;
        }
        GameMenu.instance.UpdateMenu();
    }
}