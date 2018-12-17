using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AnimalAI : MonoBehaviour
{
    [HideInInspector] public Transform[] SaveZones { get { if (zonesManager.saveZones != null) return zonesManager.saveZones; else return null; } }
    List<Transform> variantsZones = new List<Transform>();
    Transform finalZone;

    public float walkWidth;
	public float walkLength;
	[HideInInspector]
    public Transform areaCenter;
	public WalkBounds walkBounds;

	public string zoneHandlerTag;
	public ZonesManager zonesManager;
    public Transform FinalZone { get { return finalZone; } }

	protected virtual void Awake () {
        GameObject handler = GameObject.FindGameObjectWithTag(zoneHandlerTag);
        zonesManager = handler.GetComponent<ZonesManager>();
    }
    protected void Start()
    {
        
    }

    public virtual void FindSaveZone(Vector3 predatorPos)
    {
        variantsZones.Clear();
        finalZone = null;
		foreach (Transform zone in SaveZones)
        {
            if (Vector3.Angle(-Vec3Mathf.DirectionTo(transform.position,predatorPos), Vec3Mathf.DirectionTo(transform.position,zone.position)) < 130)
                variantsZones.Add(zone);
        }
        Transform[] variants = variantsZones.ToArray();
        if (variants.Length > 1)
            ChooseZone(variants);
        else
			ChooseZone(SaveZones);
    }

    void ChooseZone(Transform[] zones)
    {
        foreach (Transform zone in zones)
        {
            if (finalZone == null || Vec3Mathf.DistanceTo(transform.position,zone.position) < Vec3Mathf.DistanceTo(transform.position,finalZone.position))
                    finalZone = zone;
        }
        //Debug.Log(finalZone);
        areaCenter = finalZone;
		walkBounds.SetBounds (walkWidth, walkLength, areaCenter.position);
    }
    int zoneIndex = -1;
    public virtual Vector3 GetNextZone()
    {
        zoneIndex++;
        if (zoneIndex >= SaveZones.Length)
            zoneIndex = 0;
        return SaveZones[zoneIndex].position;
    }

	public virtual void FindAreaCenter()
    {
        int randomZone = Random.Range(0, SaveZones.Length);
        /*foreach (Transform trans in SaveZones)
        {
            if (areaCenter == null || Vec3Mathf.DistanceTo(transform.position, trans.position) < Vec3Mathf.DistanceTo(transform.position, areaCenter.position))
                areaCenter = trans;
        }*/
        areaCenter = SaveZones[randomZone];
		walkBounds.SetBounds (walkWidth, walkLength, areaCenter.position);
    }

	public void FindAreaCenter(Vector3 nearWithPos)
	{
		foreach (Transform trans in SaveZones)
		{
			if (areaCenter == null || Vec3Mathf.DistanceTo(nearWithPos, trans.position) < Vec3Mathf.DistanceTo(nearWithPos, areaCenter.position))
				areaCenter = trans;
		}
		walkBounds.SetBounds (walkWidth, walkLength, areaCenter.position);
	}

    public Vector3 GetWalkPoint()
    {
		float x = Random.Range(walkBounds.bounds.min.x, walkBounds.bounds.max.x);
		float z = Random.Range(walkBounds.bounds.min.z, walkBounds.bounds.max.z);
		Vector3 waypoint = new Vector3(x, walkBounds.bounds.center.y, z);

        return waypoint;
    }
    public Vector3 GetWalkPoint(Vector3 pos, float searchRadius)
    {
        float x = Random.Range(-searchRadius, searchRadius);
        float z = Random.Range(-searchRadius, searchRadius);
        Vector3 waypoint = new Vector3(pos.x + x, pos.y, pos.z + z);
        return waypoint;
    }


    public virtual void GetVictim( Vector3 victimPos)
    {

    }
	public struct WalkBounds {
		public Bounds bounds;
		public Vector3 topRight;
		public Vector3 bottomLeft;

		public void SetBounds (float walkWidth, float walkLenght, Vector3 center) {
			Vector3 size = new Vector3 (walkWidth, 0, walkLenght);
			bounds = new Bounds(center,size);
			bottomLeft = new Vector3 (bounds.min.x, bounds.center.y, bounds.min.y);
			topRight = new Vector3 (bounds.max.x, bounds.center.y, bounds.max.y);
			//bounds.SetMinMax (bottomLeft, topRight);
		}
	}
}