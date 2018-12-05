using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathTest : MonoBehaviour {
	
	public Transform startTransform;
	public Transform endTransform;
	NavMeshPath path;

	LineRenderer line;

	void Awake () {
		line = GetComponent<LineRenderer> ();
	}

	void Start () {
		path = new NavMeshPath ();
	}

	void Update () {
		NavMesh.CalculatePath (startTransform.position, endTransform.position, NavMesh.AllAreas, path);
		line.positionCount = path.corners.Length;
		line.SetPositions (path.corners);
		for (int i = 0; i < line.positionCount; i++) {
			Vector3 vectory = new Vector3 (line.GetPosition (i).x, line.GetPosition (i).y + 10, line.GetPosition (i).z);
			Debug.DrawLine (line.GetPosition (i), vectory);
		}
	}
}
