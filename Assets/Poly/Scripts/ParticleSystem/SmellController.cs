using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellController : MonoBehaviour {
	public Transform rabbit;
	ParticleSystem ps;
    ParticleRenderer pr;
    bool isTriggered = false;

    public delegate void MethodHandler(GameObject obj);
    public event MethodHandler OnEnter;

	void Awake () {
		ps = GetComponent<ParticleSystem>();
        pr = GetComponent<ParticleRenderer>();
	}
    void OnTriggerEnter(Collider other)
    {/*
        if (ps.isPlaying && !isTriggered)
        {
            ps.Stop();
            Debug.Log("STOP SMELL");
            
            
        }*/
        if (!isTriggered)
        {
            OnEnter(gameObject);
            isTriggered = true;
            pr.enabled = false;
        }
    }
    IEnumerator ChangeRot()
    {
        while (true)
        {
            transform.rotation = Vec3Mathf.GetDir(transform.position, rabbit.position);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
