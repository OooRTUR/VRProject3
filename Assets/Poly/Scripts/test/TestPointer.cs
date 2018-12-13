using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPointer : MonoBehaviour {
    [SerializeField] Transform canvasCenter;
    [SerializeField] Transform player;
    [SerializeField] Transform target3d;
    [SerializeField] float rad;

	// Use this for initialization
	void Start () {
        //StartCoroutine("ControlPointer");
	}
	
	// Update is called once per frame
	void Update () {

        float targetAngle = Vec3Mathf.GetAngle1(player.position, target3d.position) + Camera.main.transform.rotation.eulerAngles.y;
        transform.localPosition = Vec3Mathf.GetCirclePointXZ(canvasCenter.localPosition, targetAngle, rad);

        float selfAngle = Vec3Mathf.GetAngleCanvas(transform.localPosition, canvasCenter.localPosition);
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, selfAngle);

        //Debug.DrawLine(canvasCenter.position, new Vector3(canvasCenter.position.x, canvasCenter.position.y + 540.0f, canvasCenter.position.z));
        //Debug.DrawLine(canvasCenter.position, transform.position);

    }

    IEnumerator ControlPointer()
    {
        while (true)
        {
           
            yield return new WaitForSeconds(0.01f);
        }
    }
}
