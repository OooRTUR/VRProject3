using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveByClick : MonoBehaviour {

    [SerializeField] Camera cam;
    // serialize
    [SerializeField] NavMeshAgent agent;
    //[SerializeField] Transform target;qw
    // not serialize
    RaycastHit m_HitInfo = new RaycastHit();

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                agent.destination = m_HitInfo.point;
        }
    }
}
