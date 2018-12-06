using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchAnimal : MonoBehaviour {

	public Transform neck;
	public LayerMask animalMask;
	[Header("UI")]
	public RectTransform catchTextTrans;
	public Image fillImage;
	public Text adviseText;
	public Text mouseText,
	rabbitText, chickenText;

	int catchedM, catchedR, catchedC;
	float catchTimer;
	RaycastHit hit;

	void Update () {
		adviseText.enabled = false;
		Ray ray = new Ray (neck.position, neck.forward);
		if (Physics.SphereCast (ray, 2, out hit, 3, animalMask)) {
			adviseText.enabled = true;
			if (Input.GetMouseButton (0))
				Catch ();
			else
				ResetFill ();
		}
		else
			ResetFill ();
	}

	void Catch () {
		adviseText.enabled = false;
		catchTimer += Time.deltaTime;
		fillImage.fillAmount = catchTimer / 5;
		if (fillImage.fillAmount == 1) {
			hit.collider.gameObject.SetActive (false);
			UpdateStatUI ();
			ResetFill ();
			StartCoroutine ("MoveCatchText");
		}
	}

	void ResetFill () {
		fillImage.fillAmount = 0;
		catchTimer = 0;
	}

	void UpdateStatUI () {
		if (hit.collider.CompareTag ("Mouse")) {
			catchedM++;
			mouseText.text = catchedM.ToString();
		}
		if (hit.collider.CompareTag ("Rabbit")) {
			catchedR++;
			rabbitText.text = catchedR.ToString();
		}
		if (hit.collider.CompareTag ("Chicken")) {
			catchedC++;
			chickenText.text = catchedC.ToString();
		}
	}

	IEnumerator MoveCatchText () {
		catchTextTrans.anchoredPosition = Vector3.zero;
		Vector3 movement = new Vector3 (1, 1, 0);
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
	}
}