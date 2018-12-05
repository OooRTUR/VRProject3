using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalSteps : MonoBehaviour {

    [SerializeField]float freq;
    [SerializeField][Range(15.0f,90.0f)] float maxAngleZ = 45.0f;
	public int poolSize;
	public LayerMask groundMask;
	public GameObject stepPrefab;
	public Transform stepPos;
	float timeBetSteps;


    RotationCalculator rotCalc;
    PlaceObjectToGround place;
	Transform[] steps;
	Transform handler;
	int stepIndex = 0;
	float stepTimer;

	NavMeshAgent agent;

	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		handler = GameObject.FindGameObjectWithTag ("StepsPool").transform;
		steps = new Transform[poolSize];
		for (int i = 0; i < steps.Length; i++) {
			steps [i] = Instantiate (stepPrefab, Vector3.zero, Quaternion.identity, handler).transform;
			steps [i].gameObject.SetActive (false);
		}
        rotCalc = ScriptableObject.CreateInstance<RotationCalculator>();
        place = ScriptableObject.CreateInstance<PlaceObjectToGround>();
        SetParams();
        //StartCoroutine(DebugSteps());
	}

    void SetParams()
    {
        rotCalc.rectRad = 0.24f;
        rotCalc.rectRadZ = 0.16f;
        rotCalc.maxAngleX = 90.0f;
        rotCalc.maxAngleZ = maxAngleZ;
        rotCalc.yMod = 0.27f;
        place.corr = 0.1f;
    }

	void Update () {
		timeBetSteps = freq / agent.velocity.magnitude;
		if (stepIndex == poolSize)
			stepIndex = 0;
		if (agent.velocity.magnitude > 0) {
			stepTimer += Time.deltaTime;
			if (stepTimer > timeBetSteps) {
				stepTimer = 0;
				Ray ray = new Ray (stepPos.position, -Vector3.up);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 2f, groundMask)) {
					steps [stepIndex].position = hit.point + Vector3.up * 2.0f;
                    
                    steps[stepIndex].rotation = Vec3Mathf.GetDir(transform.position, steps[stepIndex].position);
                    float corr = Random.Range(-0.3f, 0.3f);
                    steps[stepIndex].position = new Vector3(
                        steps[stepIndex].position.x + corr,
                        steps[stepIndex].position.y,
                        steps[stepIndex].position.z
                        );

                    rotCalc.MakeCalculations(steps[stepIndex], steps[stepIndex]);
                    rotCalc.MakeCalculationsZ(steps[stepIndex], steps[stepIndex]);
                    
                    place.Place(steps[stepIndex]);
					steps [stepIndex].gameObject.SetActive (true);
					stepIndex++;
				}
			}
		}
	}
    IEnumerator DebugSteps()
    {
        while (true)
        {
            for (int i = 0; i < steps.Length; i++)
            {
                Debug.DrawLine(steps[i].position, new Vector3(
                    steps[i].position.x+2.0f, 
                    steps[i].position.y, 
                    steps[i].position.z));
            }
        yield return new WaitForEndOfFrame();
    }
}
}
