using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LogTraectoryBuilder
{
    public bool isGetEnded;
    public List<Vector3> Graph { get { return graph; } }
    public float graphCorrection = 0.0f;

    private List<Vector3> graph = new List<Vector3>();
    private List<float> speeds = new List<float>();

    private Vector3 flyingStartPos;
   

    private float x = 0.1f;
    private float y = 0.0f;
    private float z = 0.0f;
    private float c = 0.0f;
    
    private float distX;
    private float distY;
    private float distZ;
    private float distC;

    private float xMod;
    private float yMod;
    private float zMod;

    private float lgbase;
    private float stepC;
    private float stepX;
    private float stepZ;


    public LogTraectoryBuilder() {}

    public void CalcGraph(Vector3 startPoint, Vector3 endPoint)
    {
        graph.Clear();
        speeds.Clear();
        index = -1;
        isGetEnded = false;
        x = 0.1f; y = 0; z = 0;
        
        flyingStartPos = startPoint;
        distX = endPoint.x - startPoint.x;
        distY = endPoint.y - startPoint.y;
        distZ = endPoint.z - startPoint.z;
        distC = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

        xMod = distX >= 0 ? 1.0f : -1.0f;
        yMod = distY >= 0 ? 1.0f : -1.0f;
        zMod = distY >= 0 ? 1.0f : -1.0f;

        lgbase = Mathf.Pow(distC, 1 / Mathf.Abs(distY));
        stepC = distC / 100.0f;
        stepX = distX * xMod / 100.0f;
        stepZ = distZ * yMod / 100.0f;
        float dist = Vector3.Distance(startPoint, endPoint);
        c = lgbase;
        for (int i = 0; i < distC/stepC+graphCorrection; i++)
        {
            x += stepX;
            z += stepZ;
            c += stepC;
            y = Mathf.Log(c, lgbase);
            graph.Add(new Vector3(flyingStartPos.x + x * xMod, flyingStartPos.y + y * yMod, flyingStartPos.z + z*zMod));
            speeds.Add(1 / (Mathf.Log(lgbase, 2.72f) * c));
        }
    }

    int index = -1;
    public Vector3 GetNextPoint()
    {
        index++;
        if (index >= graph.Count)
        {
            index = 0;
            Debug.Log("Достигли конца траектории");
            isGetEnded = true;
        }
        //Debug.Log(index);
        return graph[index];
    }
    public float GetCurrentSpeed()
    {
        return speeds[index];
    }

}
