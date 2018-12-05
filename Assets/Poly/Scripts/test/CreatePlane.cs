using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlane : MonoBehaviour {

    [SerializeField] GameObject mathpoint;
    List<Vector3> graphPoints;
    float xMod = 0.25f;
    float yMod = 1.0f;
    Plane plane;
    Vector3 xAxis;
    Vector3 xAxis2;
    Vector3 diffVec3;
    Vector3 normVec3;
    readonly float sqrt2 = 1.141f;
    Vector3 xAxisNormal;

    readonly float graphLength = 100.0f;
    Vector3 xLine = Vector3.right;
    Vector3 yDiv = Vector3.up;

    float rad = 1.0f;

    //DrawCircle drawCircle;

    void Start() {
        float angle = 45.0f;
        xAxis = new Vector3(
            Vector3.right.x*rad*Mathf.Cos(Mathf.Deg2Rad*angle),
            Vector3.up.y *rad* Mathf.Sin(Mathf.Deg2Rad * angle), 
            0.0f);
        normVec3 = new Vector3(
            0.0f,
            Vector3.up.y * sqrt2,
            0.0f);
        xAxis2 = xAxis * 2.0f;
        diffVec3 = xAxis2 - xAxis;
        xAxisNormal = new Vector3(
            xAxis.x * rad * Mathf.Cos(Mathf.Deg2Rad*90.0f),
            xAxis.y * rad * Mathf.Sin(Mathf.Deg2Rad*90.0f),
            0.0f);
        //drawCircle = new DrawCircle(transform.GetComponent<LineRenderer>());
        //drawCircle.xradius = rad;
        //drawCircle.yradius = rad;
        xLine *= graphLength;
        (Instantiate(mathpoint)).transform.position = transform.position;
        CreateSin();
        SetPointsToGameField();
    }

    private void Update()
    {
        //Debug.DrawLine(Vector3.zero, xAxis, Color.cyan); // построить xAxis = 1
        //Debug.DrawLine(xAxis2, diffVec3, Color.red); // построить diffVec3
        //Debug.Log(diffVec3);
        //Debug.DrawLine(xAxis, normVec3, Color.yellow); // нормаль?
        //Debug.DrawLine(Vector3.zero, Vector3.right*rad, Color.red);
        //Debug.DrawLine(Vector3.zero, Vector3.up*rad, Color.blue);
        DrawGraph();
        //drawCircle.Draw();
    }

    void DrawGraph()
    {
        
        
        for (int i=0; i < graphLength; i++)
        {
            Debug.DrawLine(new Vector3(0.0f+i, 0.0f, 0.0f), new Vector3(0.0f+i, 1.0f, 0.0f), Color.gray);
        }
        Debug.DrawLine(Vector3.zero, xLine, Color.yellow);
    }

    

    void SetPointsToGameField()
    {
        float x = 1.0f, y = 1.0f, z = 1.0f;
        for (int i = 0; i < graphPoints.Count; i++)
        {
            GameObject tmpObj = Instantiate(mathpoint);
            tmpObj.transform.localPosition = new Vector3(transform.localPosition.x + graphPoints[i].x * x,
                                                    transform.localPosition.y + graphPoints[i].y*y,
                                                    transform.localPosition.z + graphPoints[i].z * z);
        }
    }

    void CreateSin()
    {
        graphPoints = new List<Vector3>();
        for (int i = 0; i < 64; i +=1)
        {
            float step = i * xMod;
            graphPoints.Add(new Vector3(step, Mathf.Sin(step) * yMod,0.0f));
        }
    }
}
