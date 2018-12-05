using UnityEngine;
using System.Collections;

namespace test
{
    public class GetRandomDir : MonoBehaviour
    {
        [SerializeField]Transform victimTrans;
        [SerializeField] Transform predatorTrans;

        // расчетные поля
        Vector3 victimPos;
        Vector3 predatorPos;
        float alpha;
        float length;
        readonly float sqrt2 = 1.141f;
        float angle = 0.0f;

        private void Awake()
        {
            victimPos = victimTrans.transform.position;
            predatorPos = predatorTrans.transform.position;
            alpha = Random.Range(-30.0f, 30.0f);
            length = Random.Range(10.0f, 55.0f);

            angle = Vec3Mathf.GetAngle(victimPos, predatorPos);

            StartCoroutine(DebugLines());
        }
        IEnumerator DebugLines()
        {
            while (true)
            {
                victimPos = victimTrans.transform.position;
                predatorPos = predatorTrans.transform.position;
                Debug.DrawLine(victimPos,new Vector3(victimPos.x, victimPos.y, victimPos.z+5.0f), Color.blue);
                Debug.DrawLine(victimPos, new Vector3(victimPos.x+1.0f, victimPos.y, victimPos.z), Color.red);
                Debug.DrawLine(predatorPos, new Vector3(predatorPos.x, predatorPos.y, predatorPos.z + 5.0f), Color.blue);
                Debug.DrawLine(predatorPos, new Vector3(predatorPos.x+1.0f, predatorPos.y, predatorPos.z), Color.red);

                Vector3 dir = Vec3Mathf.GetReverseDir(victimPos, predatorPos, length);
                Debug.DrawLine(victimPos, dir,Color.cyan);

                Vector3 dirRand = Vec3Mathf.GetReverseDir(victimPos, predatorPos, length, alpha);
                Debug.DrawLine(victimPos, dirRand, Color.yellow);

                yield return null;
            }
        }
        Vector3 GetDir(Vector3 start, Vector3 end)
        {
            float angle = Vec3Mathf.GetAngle(end, start);
            float dirX = start.x +  Mathf.Cos((angle + 90.0f) * Mathf.Deg2Rad)*length;
            float dirZ = start.z +  Mathf.Sin((angle + 90.0f) * Mathf.Deg2Rad)* length;
            Vector3 dir = new Vector3(dirX, start.y, dirZ);

            float dirAX = start.x + Mathf.Cos((angle + 90.0f+alpha) * Mathf.Deg2Rad) * length;
            float dirAZ = start.z + Mathf.Sin((angle + 90.0f+alpha) * Mathf.Deg2Rad) * length;
            Vector3 dirA = new Vector3(dirAX, start.y, dirAZ);

            //float dirA1X = start.x + Mathf.Cos((angle + 90.0f - alpha) * Mathf.Deg2Rad) * length;
            //float dirA1Z = start.z + Mathf.Sin((angle + 90.0f - alpha) * Mathf.Deg2Rad) * length;
            //Vector3 dir1A = new Vector3(dirA1X, start.y, dirA1Z);
            Debug.DrawLine(start, dirA, Color.yellow);
            //Debug.DrawLine(start, dir1A, Color.magenta);

            return dir;
        }


    }
}
