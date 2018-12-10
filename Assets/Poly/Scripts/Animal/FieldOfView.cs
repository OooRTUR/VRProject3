using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FieldOfView : MonoBehaviour {
	
	public bool checkObstacles = true;
	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[HideInInspector]public List<Transform> visibleTargets = new List<Transform>();


    //public bool isEnemySpotted { get { return visibleTargets.Count > 0 ? true : false; } }
    float spottingTime;


	void Awake () {
		StartCoroutine(FindTargetsWithDelay(0.3f));
	}

	void Update () {
        //Debug.Log(visibleTargets.Count);
        spottingTime += Time.deltaTime;
	}

	IEnumerator FindTargetsWithDelay (float delay) {
		while(true) {
			FindVisibleTargets();
			yield return new WaitForSeconds(delay);
		}
	}

	void FindVisibleTargets () {
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        if (targetsInView.Length == 0) visibleTargets.Clear();
        for (int i = 0; i < targetsInView.Length; i++)
        {
            Transform target = targetsInView[i].transform;
            Vector3 dir = (target.position - transform.position).normalized;
            CharacterController c_controller = targetsInView[i].GetComponent<CharacterController>();
            //VRTK.SDK_InputSimulator inputSimulator = targetsInView[i].GetComponent<VRTK.SDK_InputSimulator>();
            float dist = Vector3.Distance(transform.position, target.position);
            if (Vector3.Angle(transform.forward, dir) < viewAngle / 2 || c_controller.velocity.magnitude > 10f)
            {
				if ((Physics.Raycast(transform.position, dir, dist, obstacleMask) && checkObstacles) && visibleTargets.Contains(target)) // && !visibleTargets.Exists(trans => trans == target) - доп проверка на всякий
                    visibleTargets.Remove(target);
				else if ((!Physics.Raycast(transform.position, dir, dist, obstacleMask) || !checkObstacles) && !visibleTargets.Contains(target))
                    visibleTargets.Add(target);
            }
        }
    }

    public Vector3 DirFromAngle (float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) angleInDegrees += transform.eulerAngles.y;
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
