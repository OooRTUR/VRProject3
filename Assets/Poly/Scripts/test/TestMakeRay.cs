using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMakeRay : MonoBehaviour
{

    float maxDist = 4.0f;
    int layerMask = 1 << 9;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.down;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, maxDist, layerMask))
        {
            print("Ground is hitted");
            Debug.DrawRay(transform.position, dir * hit.distance, Color.green);
        }
        else
        {
            print("Ground is not hitted");
            Debug.DrawRay(transform.position, dir * maxDist, Color.red);
        }

    }
}