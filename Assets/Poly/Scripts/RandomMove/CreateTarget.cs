using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTarget : MonoBehaviour {

    Vector3 startPosition;

    public Transform targetObject;

    float radius = 5.0f;

    private void Awake()
    {
        startPosition = transform.position;
    }

    // Use this for initialization
    void OnEnable () {
        Vector3 randomPoint = new Vector3(
            startPosition.x + Random.Range(-10.0f, 10.0f),
            startPosition.y,
            startPosition.z + Random.Range(-10.0f, 10.0f)
            );
        //Vector3 vec3 = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10.0f);
        targetObject.position = randomPoint;
        GetComponent<CreateRotation>().enabled = true;
        GetComponent<CreateTarget>().enabled = false;
    }
}
