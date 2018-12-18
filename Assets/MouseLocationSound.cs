using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocationSound : MonoBehaviour {
    public Transform rabbit;
    public delegate void MethodHandler(GameObject obj);
    public event MethodHandler OnEnter;

    bool isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            OnEnter(gameObject);
            isTriggered = true;
            gameObject.SetActive(false);
            
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
