using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Trap : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		other.GetComponent<SDK_InputSimulator>().isMoveble = false;
		Debug.Log("You're Dead!");
	}
}
