using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour {
    [SerializeField] BoxCollider box;
    [SerializeField] BoxCollider trigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Mouse")
        {
            box.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Mouse")
        {
            box.enabled = false;
        }
    }
}
