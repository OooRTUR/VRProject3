using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AnimalAI))]
public class AreaOfWalkEditor : Editor {

	void OnSceneGUI () {
		AnimalAI aow = (AnimalAI)target;
		Handles.color = Color.green;
		if (aow.areaCenter != null) {
			Handles.DrawWireCube (aow.walkBounds.bounds.center, aow.walkBounds.bounds.size);
			Handles.color = Color.blue;
			Handles.DrawWireArc (aow.walkBounds.bounds.max, Vector3.up, Vector3.forward, 360, 2);
			Handles.DrawWireArc (aow.walkBounds.bounds.min, Vector3.up, Vector3.forward, 360, 2);
		}
	}
}
