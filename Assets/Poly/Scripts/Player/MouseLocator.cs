using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocator : MonoBehaviour {
	//[SerializeField] AudioClip mouseSound;
	[SerializeField] int minTime;
	[SerializeField] int maxTime;
	Transform[] mouseTrans;
	AudioSource s_source;

	void Awake () {
		GameObject[] mouses = GameObject.FindGameObjectsWithTag ("Mouse");
		mouseTrans = new Transform[mouses.Length];
		for (int i = 0; i < mouseTrans.Length; i++) {
			mouseTrans [i] = mouses [i].transform;
		}
		s_source = GetComponent<AudioSource>();
		//s_source.clip = mouseSound;
	}

	void OnEnable () {
		StartCoroutine("HearMouse");
	}

	void OnDisable () {
		StopCoroutine("HearMouse");
	}

	IEnumerator HearMouse () {
		while (true) {
			FindMouse();
			int timeBetween = Random.Range(minTime,maxTime);
			yield return new WaitForSeconds(timeBetween);
		}
	}

	void FindMouse () {
		int mousesInDir = 0;
		foreach (Transform mouse in mouseTrans) {
			Vector3 dir = Vec3Mathf.DirectionTo (transform.position, mouse.position);
			if (Vector3.Angle (transform.forward, dir) < 20)
				mousesInDir ++;
		}
		if (mousesInDir > 0)
			s_source.volume = 1;
		else
			s_source.volume = 0.3f;
		//s_source.Play();
	}


}
