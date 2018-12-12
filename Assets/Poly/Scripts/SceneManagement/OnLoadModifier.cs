using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadModifier : MonoBehaviour {


	void Start () {
        if (OnLoadManager.instance.mainMenuMode)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
	}
}
