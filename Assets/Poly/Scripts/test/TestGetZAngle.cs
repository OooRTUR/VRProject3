using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetZAngle : MonoBehaviour {
    [SerializeField] Transform targ;
    [SerializeField] string angle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(transform.position, targ.position);
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z+5.0f));
       angle = Vec3Mathf.GetAngleZY(transform.position, targ.position).ToString();
	}
}
