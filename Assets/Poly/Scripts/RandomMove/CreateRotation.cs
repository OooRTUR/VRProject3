using System;
using System.Collections.Generic;
using UnityEngine;

class CreateRotation : MonoBehaviour
{

    public Transform target;
    [HideInInspector]public float speed;

    

    Vector3 targetDir;
    Vector3 rotation;

    float angle;
    Vector3 myLerp;

    float modifier;

    [HideInInspector] public bool angleReached;

    private void OnEnable()
    {
        //myLerp = Vector3.Lerp(targetDir, transform.forward, 1.0f);
        
        //if (myLerp.x < 0.0f)
        //    modifier = -1.0f;
        //else
        //    modifier = 1.0f;

        //Debug.LogFormat("<color=red>lerp: {0} | modifier: {1}</color>", myLerp.x,modifier);
        targetDir = target.position - transform.position;
        angle = Vector3.Angle(targetDir, transform.forward);
        
    }

    private void Update()
    {
        if(Time.time > 2.5f)
        {
            MakeRot();
        }
    }

    void MakeRot()
    {
        targetDir = target.position - transform.position;
        angle = Vector3.Angle(targetDir, transform.forward);

        //Quaternion qRot = Quaternion.LookRotation(targetDir, Vector3.up);
        //Debug.Log("qRot: " + qRot);

        rotation = new Vector3(transform.rotation.x, angle, transform.rotation.y);
        //Debug.Log("lerp: "+myLerp+" | angle: "+angle);
        if (angle > 5.0f) angleReached = false;
        else angleReached = true;
        if (!angleReached)
        {
            transform.Rotate(rotation * Time.deltaTime * speed);
        }
        else
        {
            //Debug.Log("Угол вращения достигнут");
            angleReached = true;
            GetComponent<CreateMove>().enabled = true;
            GetComponent<CreateRotation>().enabled = false;
        }
    }
}
