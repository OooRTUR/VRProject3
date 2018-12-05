using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesManager : MonoBehaviour {

    [SerializeField] public SetTrailsZone trailStart;
    [HideInInspector] public Transform[] saveZones;
    [SerializeField] public Transform eaglePatrolPoint;


    [SerializeField] public Transform wayPointsHandler;
    [SerializeField] Transform[] chickenTreeZones;
    
    


    private void Awake()
    {
        saveZones = new Transform[wayPointsHandler.childCount];
        for (int i = 0; i < saveZones.Length; i++)
        {
            saveZones[i] = wayPointsHandler.GetChild(i);
            // Debug.Log(transform.name+" | " + saveZones[i].name);
        }
    }
    public Vector3 GetSaveZone(Vector3 position)
    {
        Transform minPos = chickenTreeZones[0];
        for(int i=1; i < chickenTreeZones.Length; i++)
        {
            if (Vector3.Distance(minPos.position, position) > Vector3.Distance(chickenTreeZones[i].position,position))
                minPos = chickenTreeZones[i];
        }
        Debug.Log(minPos.name);
        return minPos.position;
    }
    
}
