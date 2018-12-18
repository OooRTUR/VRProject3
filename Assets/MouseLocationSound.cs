using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocationSound : MonoBehaviour {
    public Transform rabbit;
    public delegate void MethodHandler(GameObject obj);
    public event MethodHandler OnEnter;
    Renderer rend;
    bool isTriggered = false;


    private void Start()
    {
        //mat = GetComponent<Renderer>().material;
        rend = GetComponent<Renderer>();
    }

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
        float playerRot = 360.0f - Player.instance.transform.rotation.eulerAngles.y;
        float rotToTarget = Vec3Mathf.GetAngle1(Player.instance.transform.position, transform.position);
        float deltaRot = playerRot - rotToTarget;

        if(Mathf.Abs(deltaRot) < 30.0f)
        {
            rend.enabled = true;
        }
        else
        {
            rend.enabled = false;
        }

        Debug.Log(playerRot + " | " + rotToTarget + " | " + deltaRot);
	}
}
