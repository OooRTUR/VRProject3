using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFeatherRandomly : MonoBehaviour {

    

    [SerializeField] float modifier = 0.7f;
    [SerializeField] GameObject traectoryPointObj;
    [SerializeField] float angle;
    List<Vector2> graphPoints;

    private void Awake()
    {
        CreateMathGraph();
        SetPointsToGameField();
        transform.Rotate(Vector3.up * 300.0f, Space.Self);
        //ListSetter.SetList();
    }
    
    void SetPointsToGameField()
    {
        for(int i=0; i<graphPoints.Count; i++)
        {
            GameObject tmpObj = Instantiate(traectoryPointObj, transform,true);
            tmpObj.transform.localPosition = new Vector3(transform.localPosition.x+graphPoints[i].x * 2.0f,
                                                    transform.localPosition.y+0.7f, 
                                                    transform.localPosition.z+ graphPoints[i].y*1.1f);
        }
    }

    void CreateMathGraph()
    {
        graphPoints = new List<Vector2>();
        for (int i = 0; i < 32; i+=2)
        {
            float step = i * modifier;
            graphPoints.Add(new Vector2(step, Mathf.Sin(step)*0.6f));
        }
        //for(int i=0; i<graphPoints.Count; i++)
        //{
        //    Debug.Log(graphPoints[i]);
        //}
    }
}
