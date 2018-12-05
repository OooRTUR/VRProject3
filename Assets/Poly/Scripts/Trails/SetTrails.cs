using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetTrails : MonoBehaviour
{

    public Transform startTransform;
    public Transform endTransform;
    public GameObject trail;

    PlaceObjsByCurve placeByCurve;
    NavMeshPath path;
    Vector3[] pathCorners;
    LineRenderer line;
    bool debugMode = false;

    public float corr = 0.0f;
    [SerializeField] public float rad = 3.0f;
    [SerializeField] public float dim = 2.0f;
    [SerializeField] int rar = 1;

    public void Run()
    {

        path = new NavMeshPath();

        NavMesh.CalculatePath(startTransform.position, endTransform.position, NavMesh.AllAreas, path);
        pathCorners = new Vector3[path.corners.Length];
        pathCorners = path.corners;
        if (debugMode)
        {
            if (GetComponent<LineRenderer>() != null) line = GetComponent<LineRenderer>();
            StartCoroutine("DrawPath");
        }
        Init();
    }
    private void Init()
    {
        float uplift = 15.0f;
        placeByCurve = ScriptableObject.CreateInstance<PlaceObjsByCurve>();
        placeByCurve.rad = rad;
        placeByCurve.dim = dim;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            //Debug.Log(i);
            //Debug.Log(pathCorners[i].y + " | " + pathCorners[i+1].y);
            Vector3 nullY1 = new Vector3(pathCorners[i].x, 0.0f + uplift, pathCorners[i].z);
            Vector3 nullY2 = new Vector3(pathCorners[i + 1].x, 0.0f + uplift, pathCorners[i + 1].z);
            placeByCurve.rar = rar;
            placeByCurve.Run(
                nullY1,
                nullY2);
            placeByCurve.objsParent = transform;
            placeByCurve.PlaceObjByGraph(trail);
            //StartCoroutine(placeByCurve.DebugLines());
        }

    }
    IEnumerator DrawPath()
    {
        while (true)
        {
            NavMesh.CalculatePath(startTransform.position, endTransform.position, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            for (int i = 0; i < line.positionCount - 1; i++)
            {

                Vector3 vectory = new Vector3(line.GetPosition(i).x, line.GetPosition(i).y + 10, line.GetPosition(i).z); // draw y
                Debug.DrawLine(line.GetPosition(i), vectory);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
