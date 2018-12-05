using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchAnimal : MonoBehaviour {

	public Transform neck;
	public LayerMask animalMask;
	[Header("UI")]
	public Image fillImage;
	public Text mouseText,
	rabbitText, chickenText;

	int catchedM, catchedR, catchedC;
	float catchTimer;
	RaycastHit hit;

	void Update () {
		Ray ray = new Ray (neck.position, neck.forward);
		if(Physics.SphereCast (ray, 2, out hit, 3, animalMask) && Input.GetMouseButton(0))
			Catch ();
		else
			ResetFill ();
	}

	void Catch () {
		catchTimer += Time.deltaTime;
		fillImage.fillAmount = catchTimer / 5;
		if (fillImage.fillAmount == 1) {
			hit.collider.gameObject.SetActive (false);
			UpdateUI ();
			ResetFill ();
		}
	}

	void ResetFill () {
		fillImage.fillAmount = 0;
		catchTimer = 0;
	}

	void UpdateUI () {
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
}
