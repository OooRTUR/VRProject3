using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyByLnTestMath : MonoBehaviour {

    [SerializeField]Transform flying;
    [SerializeField]Transform startPoint;
    [SerializeField] Transform endPoint;
    Vector3 flyingPos;
    [SerializeField]float speed = 0.0f;
    [SerializeField]int pointCount = 100;
    LogTraectoryBuilder traectory = new LogTraectoryBuilder();

    // Use this for initialization
    void Start () {
        //StartCoroutine(Fly());
        StartCoroutine(Fly1());
        
    }


    IEnumerator Fly1()
    {
        while (true)
        {
            flying.position = startPoint.position;
            traectory.CalcGraph(startPoint.position, endPoint.position);
            Vector3 nextP = traectory.GetNextPoint();
            while (!traectory.isGetEnded)
            {
                //speed = speeds[(int)k];
                if(Vector3.Distance(flying.position, nextP) <0.1f)
                    nextP = traectory.GetNextPoint();
                //Debug.Log(traectory.GetCurrentSpeed());
                flying.transform.position = Vector3.MoveTowards(flying.transform.position, nextP, 1.0f);
                Debug.DrawLine(flying.transform.position, nextP, Color.red);
                for (int i = 1; i < traectory.Graph.Count; i++)
                {
                    Debug.DrawLine(traectory.Graph[i - 1], traectory.Graph[i]);
                }
                yield return new WaitForSeconds(0.01f);
            }
            Debug.Log("Перезапуск полета");
            yield return null;
        }
    }

    IEnumerator Fly()
    {
        while (true)
        {
            List<Vector3> graph = new List<Vector3>();
            List<float> speeds = new List<float>();
            float x = 0.1f;
            float y = 0.0f;
            float z = 0.0f;
            float c = 0.0f;

            Vector3 flyingStartPos = startPoint.position;//flying.position;
            float distX = endPoint.position.x - startPoint.position.x;
            float distY = endPoint.position.y - startPoint.position.y;
            float distZ = endPoint.position.z - startPoint.position.z;
            float distC = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

            float xMod = distX >= 0 ? 1.0f : -1.0f;
            float yMod = distY >=0 ? 1.0f: -1.0f;
            float zMod = distZ >= 0 ? 1.0f : -1.0f;

            float dist = Vector3.Distance(startPoint.position, endPoint.position);
            float lgbase = Mathf.Pow(distC, 1 / Mathf.Abs(distY));
            float stepC = distC / 100.0f;
            float stepX = distX * xMod / 100.0f;
            float stepZ =  distZ * yMod / 100.0f;
            
            //Debug.Log("stepc: "+distC + "stepX"+distX);
            //Debug.Log(stepX + " " + stepZ);
            c = lgbase;
            for (int i = 0; i < distC; i++)
            {
                x += stepX;
                z += stepZ;
                c += stepC;
                y = Mathf.Log(c, lgbase);
                graph.Add(new Vector3(flyingStartPos.x+x*xMod, flyingStartPos.y+y*yMod-2.0f, flyingStartPos.z+z));
                speeds.Add(1 /( Mathf.Log(lgbase,2.72f)*c));
            }
            float k = 0;
            
            while ( k <graph.Count)
            {
                speed = speeds[(int)k];
                flying.transform.position = Vector3.MoveTowards(flying.transform.position, graph[(int)k], 0.5f/(speed));
                for (int i = 1; i < graph.Count; i++)
                {
                    Debug.DrawLine(graph[i - 1], graph[i]);
                }
                Debug.DrawLine(flyingStartPos, new Vector3(flyingStartPos.x, flyingStartPos.y+ distY, flyingStartPos.z));
                Debug.DrawLine(flyingStartPos, new Vector3(flyingStartPos.x+distX, flyingStartPos.y, flyingStartPos.z + distZ));
                k +=1;
                yield return new WaitForEndOfFrame();
            }
            flying.position = flyingStartPos;
            yield return null;
        }
    }
}
