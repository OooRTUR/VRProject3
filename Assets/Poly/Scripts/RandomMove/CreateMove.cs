using System;
using System.Collections.Generic;
using UnityEngine;


class CreateMove : MonoBehaviour
{
    public Transform target;
    [HideInInspector]public float speed;

    [HideInInspector] public bool destinationReached;

    private void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist <= 0) destinationReached = true;
        else destinationReached = false;



        if (!destinationReached)
        {
            // The step size is equal to speed times frame time.
            float step = speed * Time.deltaTime;
            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            //Debug.Log("Object is moving | position: " + transform.position + "target position: " + target.position);
        }
        else
        {
            //Debug.Log("Destination reached");
            GetComponent<MoveNPCRandomly>().destinationReached = true;
            GetComponent<MoveNPCRandomly>().enabled = true;
            GetComponent<CreateMove>().enabled = false;
        }
    }
}
