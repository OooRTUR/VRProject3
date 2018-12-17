using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjsByCurve : ScriptableObject {

    public Transform objsParent;
    public float graphLength = 0;
    public float rad = 2.5f;
    public float dim = 2.0f;
    public int rar = 4;
    public float corr;
    public float offset;

    Vector3 startPos;
    Vector3 targPos;
    PlaceObjectToGround place;
    RotationCalculator rotCalc;
    Transform start;
    Transform target;
    readonly float sqrt2 = 1.141f;
    readonly int limit = 0;
    float angle = 0.0f;
    Vector3[] vec3points;
    


    public void Run(Vector3 start, Vector3 target)
    {
        startPos = start;
        targPos = target;

        graphLength = (Vector3.Distance(startPos, targPos) / rad) - limit;
        //Debug.Log(graphLength);
        if (graphLength >= 1)
            vec3points = new Vector3[(int)graphLength];
        else
            vec3points = new Vector3[1];
        angle = Vec3Mathf.GetAngle(start, target);
        //Debug.Log(angle);
        //Debug.Log("start: "+start +"angle: "+ target);
        CalcGraph();
        if (graphLength < 5)
            rar = 1;
    }

    
    public IEnumerator DebugLines()
    {
        while (true)
        {
            angle = Vec3Mathf.GetAngle(startPos, targPos);
            Debug.DrawLine(startPos, new Vector3(startPos.x + 5.0f, startPos.y, startPos.z), Color.red); // x axis
            Debug.DrawLine(startPos, new Vector3(startPos.x, startPos.y, startPos.z + 5.0f), Color.blue); // z axis
            Debug.DrawLine(startPos, GetVec3(graphLength), Color.cyan); // rotation vector
            CalcGraph();
            DrawGraph();
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void PlaceObjByGraph(GameObject obj)
    {
        place = ScriptableObject.CreateInstance<PlaceObjectToGround>();
        rotCalc = ScriptableObject.CreateInstance<RotationCalculator>();
        rotCalc.rectRad = 1.0f;
        rotCalc.rectRadZ = 1.0f;
        rotCalc.yMod = 2.0f;
        rotCalc.maxAngleX = 60.0f;
        rotCalc.maxAngleZ = 45.0f;
        //Debug.Log("graph length: "+graphLength);
        GameObject tempObj;
        float rnd = 0.2f;
        for (int i = 0; i < vec3points.Length-1; i++)
        {
            if (i % rar == 0)
            {
                tempObj = Instantiate(obj, objsParent);
                tempObj.transform.position = new Vector3(vec3points[i].x, vec3points[i].y+offset, vec3points[i].z);
                tempObj.transform.parent = objsParent;

                rnd = 0.2f;
                tempObj.transform.rotation = new Quaternion(
                    tempObj.transform.rotation.x,
                    tempObj.transform.rotation.y + Random.Range(-rnd, rnd),
                    tempObj.transform.rotation.z,
                    tempObj.transform.rotation.w
                    );
                tempObj.transform.rotation = Vec3Mathf.GetDir(vec3points[i], vec3points[0]);
                rotCalc.MakeCalculations(tempObj.transform, tempObj.transform);
                place.corr = corr;
                place.Place(ref tempObj);
            }
        }

        tempObj = Instantiate(obj, objsParent);
        tempObj.transform.position = new Vector3(vec3points[vec3points.Length-1].x, vec3points[vec3points.Length - 1].y + offset, vec3points[vec3points.Length - 1].z);
        tempObj.transform.parent = objsParent;

        
        tempObj.transform.rotation = new Quaternion(
            tempObj.transform.rotation.x,
            tempObj.transform.rotation.y + Random.Range(-rnd, rnd),
            tempObj.transform.rotation.z,
            tempObj.transform.rotation.w
            );
        //tempObj.transform.rotation = Vec3Mathf.GetDir(vec3points[vec3points.Length - 3], vec3points[vec3points.Length-2]);
        rotCalc.MakeCalculations(tempObj.transform, tempObj.transform);
        place.corr = corr;
        place.Place(ref tempObj);
    }

    public void PlaceObjByGraph(GameObject obj1, GameObject obj2)
    {
        place = ScriptableObject.CreateInstance<PlaceObjectToGround>();
        //Debug.Log("graph length: "+graphLength);
        for (int i=0; i < vec3points.Length; i++)
        {
            if(i%rar == 0)
            {
                GameObject tempObj = Instantiate(obj1, start);
                tempObj.transform.position = vec3points[i];
                place.Place(ref tempObj);
            }
            //Debug.Log(transform.name+" | i: "+i+" | " +vec3points[i]);
            
            

            if(i % 10 == 0 & i!=0)
            {
                GameObject tempObj2 = Instantiate(obj2, start);
                tempObj2.transform.position = vec3points[i];
                place.Place(ref tempObj2);
                tempObj2.transform.position = new Vector3(tempObj2.transform.position.x, tempObj2.transform.position.y + 4.5f, tempObj2.transform.position.z);
            }
        }
    }

    //методы отображения

    Vector3 GetVec3(float mod)
    {
        return new  Vector3(
            startPos.x + mod * rad * Mathf.Cos(Mathf.Deg2Rad * (angle+90.0f)),
            startPos.y,
            startPos.z + mod * rad * Mathf.Sin(Mathf.Deg2Rad * (angle+90.0f)));
    }
    void DrawGraph()
    {
        for(int i=0; i < vec3points.Length; i++)
        {
            Debug.DrawLine(GetVec3(i), vec3points[i], Color.red);
        }
    }

    // расчетные методы

    public void CalcGraph()
    {
        vec3points[0] = new Vector3(startPos.x, startPos.y, startPos.z);
        for(int i=1; i < vec3points.Length; i++)
        {
            // В dim 
            float dim = Mathf.Sin(Mathf.Rad2Deg * i) * this.dim;
            //float dim = 5.0f;
            Vector3 vec3 = CalcVec3(i, 1, dim);
            vec3points[i] = new Vector3(startPos.x + vec3.x, startPos.y, startPos.z + vec3.z);
        }
        //Debug.Log(graphLength);
        //Debug.Log(dim);
        //Debug.Log(rad);
    }

    //dim отвечает за "ширину" графа, т.е. максимальные точки графика с большим значением dim будут сильнее отклонены от оси x
    Vector3 CalcVec3(float radmod, float modifier, float dim)
    {
        
        float radX = rad * radmod;
        float x, y, z;
        float angle_s= Mathf.Atan(dim / radX);
        float c = Mathf.Sqrt(dim * dim + radX * radX);

        x = c * Mathf.Cos(Mathf.Deg2Rad*(angle+90.0f) + angle_s * modifier);
        z = c * Mathf.Sin(Mathf.Deg2Rad * (angle+90.0f) + angle_s * modifier);

        return new Vector3(x, startPos.y, z);
    }

}
