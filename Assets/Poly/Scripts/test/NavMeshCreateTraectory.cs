using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshCreateTraectory : MonoBehaviour {

    NavMeshAgent agent;
    [SerializeField]float remDist;
    [SerializeField]float rad;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
        StartCoroutine(MoveByCircle());
        StartCoroutine(DrawTraectory());
	}
    Vector3 startPos;
    IEnumerator MoveByCircle()
    {
        float angle = 0.0f;
        
        agent.SetDestination(Vec3Mathf.GetCirclePoint(startPos, angle, rad));
        while (true)
        {

            if (agent.remainingDistance < remDist)
            {
                angle += 5.0f;
                agent.SetDestination(Vec3Mathf.GetCirclePoint(startPos, angle, rad));
                Debug.Log("Добавляю угол");
            }
            
            yield return new WaitForSeconds(0.01f);
        }
    }

    List<Vector3> traectory;
    IEnumerator DrawTraectory()
    {
        traectory = new List<Vector3>();
        while (true)
        {
            Debug.DrawLine(startPos, agent.destination,Color.red);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
