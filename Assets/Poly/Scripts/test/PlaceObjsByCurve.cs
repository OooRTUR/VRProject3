using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test
{
    public class PlaceObjsByCurve : MonoBehaviour
    {
        PlaceObjectToGround place;
        readonly float sqrt2 = 1.141f;

        [SerializeField] float rad = 1.0f;
        [SerializeField] float angle = 45.0f;
        [SerializeField] float dim = 0.1f;
        int graphLength = 50;
        readonly int limit = 10;
        [SerializeField] Transform target;
        [SerializeField] GameObject obj1;
        [SerializeField] GameObject obj2;

        DrawCircle drawCircle1;
        Vector3 vec3;
        Vector3 yVec3;
        Vector3 normVec3;
        Vector3 bis;
        Vector3 pointZero;

        Vector3 vec_1a;
        Vector3 vec_2a;
        Vector3 vec_3a;

        Vector3[] vec3points;
        Vector3[] vec3points2;

        void Start()
        {
            place = new PlaceObjectToGround();
            drawCircle1 = new DrawCircle(GetComponent<LineRenderer>());
            yVec3 = new Vector3(
                0.0f,
                Vector3.up.y * sqrt2,
                0.0f);

            //normVec3 = vec3 + yVec3;
            //vec_1a = CalcVec3(1,1);
            //vec_2a =  CalcVec3(2,1);
            graphLength = (int)(Vector3.Distance(transform.position, target.position) / rad) - limit;
            Debug.Log("dist to target" + graphLength);
            vec3points = new Vector3[graphLength];
            angle = GetAngle();
            CalcGraph();
            PlaceObjByGraph();


        }

        void Update()
        {
            drawCircle1.Draw();


            //if (Vector3.Angle(body.right, target.position - body.position) > 90f) side = "left"; else side = "right";
            //angle = angle360(transform.position, target.position, Vector3.right);
            Debug.Log("angle:" + angle);
            //angle = transform.rotation.eulerAngles.y + 90.0f;
            //Debug.Log(transform.rotation.eulerAngles.y);
            angle = GetAngle();
            bis = new Vector3(
                transform.position.x + 1.0f * Mathf.Cos(Mathf.Deg2Rad * 45.0f),
                transform.position.y,
                transform.position.z + 1.0f * Mathf.Sin(Mathf.Deg2Rad * 45.0f)
                );

            Debug.DrawLine(transform.position, new Vector3(transform.position.x + 50.0f, transform.position.y, transform.position.z), Color.red); // x axis
            Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 50.0f), Color.blue); // z axis
            Debug.DrawLine(transform.position, bis, Color.yellow);
            Debug.DrawLine(transform.position, GetVec3(2.0f), Color.cyan);
            //Debug.DrawLine(transform.position, GetVec3(2.0f), Color.cyan);
            //Debug.DrawLine(transform.position, GetVec3(3.0f), Color.cyan);
            //Debug.DrawLine(transform.position, GetVec3(4.0f), Color.cyan);

            //Debug.DrawLine(transform.position, vec3 * 0.0f, Color.cyan);

            //vec_1a = transform.position +  CalcVec3(1,1,1);
            //Debug.DrawLine(transform.position, vec_1a, Color.cyan);
            //Debug.DrawLine(GetVec3(1.0f),vec_1a,Color.yellow);

            //vec_2a = transform.position + CalcVec3(2, 1, 1);
            //Debug.DrawLine(transform.position, vec_2a, Color.cyan);
            //Debug.DrawLine(GetVec3(2.0f), vec_2a, Color.yellow);

            CalcGraph();
            DrawGraph();

            //Debug.DrawLine(Vector3.zero, vec3*4, Color.cyan);
            //Debug.DrawLine(Vector3.zero, vec_2a, Color.red);
            //Debug.DrawLine(vec3 * 1, vec_1a, Color.grey);
            //Debug.DrawLine(vec3 * 2, vec_2a, Color.grey);
            //Debug.DrawLine(vec3, yVec3, Color.yellow);
        }

        void PlaceObjByGraph()
        {
            Debug.Log("graph length: " + graphLength);
            for (int i = 0; i < graphLength; i++)
            {
                GameObject tempObj = Instantiate(obj1, transform);
                tempObj.transform.position = vec3points[i];

                place.Place(ref tempObj);

                if (i % 4 == 0)
                {
                    GameObject tempObj2 = Instantiate(obj2, transform);
                    tempObj2.transform.position = vec3points[i];
                    place.Place(ref tempObj2);
                    tempObj2.transform.position = new Vector3(tempObj2.transform.position.x, tempObj2.transform.position.y + 4.5f, tempObj2.transform.position.z);
                }
            }
        }

        float GetAngle()
        {
            Vector3 targetDir = target.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);
            if (!(Vector3.Angle(transform.right, target.position - transform.position) > 90f))
                angle = 360.0f - angle;
            return angle;
        }

        Vector3 GetVec3(float mod)
        {
            return new Vector3(
                transform.position.x + mod * rad * Mathf.Cos(Mathf.Deg2Rad * (angle + 90.0f)),
                transform.position.y,
                transform.position.z + mod * rad * Mathf.Sin(Mathf.Deg2Rad * (angle + 90.0f)));
        }
        void DrawGraph()
        {
            Debug.DrawLine(Vector3.zero, vec3 * graphLength, Color.red);
            for (int i = 0; i < graphLength; i++)
            {
                Debug.DrawLine(GetVec3(i), vec3points[i], Color.grey);
            }
        }

        void CalcGraph()
        {
            for (int i = 0; i < graphLength; i++)
            {
                float dim = Mathf.Sin(Mathf.Rad2Deg * i) * this.dim;
                Vector3 vec3 = CalcVec3(i, 1, dim);
                vec3points[i] = new Vector3(transform.position.x + vec3.x, transform.position.y, transform.position.z + vec3.z);
            }
        }

        Vector3 CalcVec3(float radmod, float modifier, float dim)
        {

            float radX = rad * radmod;
            float x, y, z;
            float angle_s = Mathf.Atan(dim / radX);
            float c = Mathf.Sqrt(dim * dim + radX * radX);

            x = c * Mathf.Cos(Mathf.Deg2Rad * (angle + 90.0f) + angle_s * modifier);
            z = c * Mathf.Sin(Mathf.Deg2Rad * (angle + 90.0f) + angle_s * modifier);

            return new Vector3(x, transform.position.y, z);
        }

    }
}