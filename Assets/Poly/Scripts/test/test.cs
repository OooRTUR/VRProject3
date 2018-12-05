using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTest : MonoBehaviour {

    public Transform target;
    float angle;
	// Use this for initialization
	void Start () {
        //transform.position = Vector3.Cross(transform.position,Vector3.up);
        Debug.Log("rot: " + transform.rotation.eulerAngles + "locRot: " + transform.localRotation.eulerAngles);
        //Quaternion rotation = Quaternion.Euler(0, 90, 0);
        //transform.rotation = rotation;
        //Debug.Log("rot: " + transform.rotation.eulerAngles + "locRot: " + transform.localRotation.eulerAngles);

        Vector3 targetDir = target.position - transform.position;
        angle = Vector3.Angle(targetDir, transform.forward);
        var rotation = new Vector3(transform.rotation.x, angle, transform.rotation.y);
        transform.Rotate(rotation);
        Debug.Log(angle);

        targetDir = target.position - transform.position;
        angle = Vector3.Angle(targetDir, transform.forward);
        Debug.Log(angle);
    }

    Vector3 GetNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        // Find vectors corresponding to two of the sides of the triangle.
        Vector3 side1 = b - a;
        Vector3 side2 = c - a;

        // Cross the vectors to get a perpendicular vector, then normalize it.
        return Vector3.Cross(side1, side2).normalized;
    }
}
