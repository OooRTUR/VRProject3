using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetAngleFly : MonoBehaviour {

    [SerializeField]Transform startRot;
    [SerializeField]Transform endRot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        startRot.rotation = Vec3Mathf.GetDirXY(startRot.position, endRot.position);
    }
}
