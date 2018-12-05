using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjDirectlyToPlayer : MonoBehaviour
{
    [SerializeField] GameObject feather;
    [SerializeField] GameObject smell;
    [SerializeField] Transform targetNPC;
    [SerializeField] Transform targetPlayer;
    [SerializeField] float x_mod = 0.7f;
    [SerializeField] float y_mod = 0.6f;
    float step = 7.0f;
    List<Vector2> graphPoints;

    int i = 0;

    // Speed in units per sec.
    public float speed;

    private void Awake()
    {
        Vector3 vec3 = targetPlayer.position - transform.position;
        //Debug.Log("vec3"+vec3);
        StartCoroutine(CreateMathGraph());
    }

    private void Update()
    {
        //The step size is equal to speed times frame time.
        //float step = speed * Time.deltaTime;

        //Move our position a step closer to the target.
       //targetNPC.position = Vector3.MoveTowards(targetNPC.position, targetPlayer.position, step);
       // float dist = Vector3.Distance(targetPlayer.position, targetNPC.position);
       // print("Distance to other: " + dist);
    }

    IEnumerator CreateMathGraph()
    {
        //graphPoints = new List<Vector2>();
        while (true)
        {
            i++;
            targetNPC.position = Vector3.MoveTowards(targetNPC.position, targetPlayer.position, step);

            GameObject tmpFeather = Instantiate(targetNPC.gameObject, transform.parent);
            tmpFeather.transform.position = new Vector3(
                targetNPC.position.x + Random.Range(-2.5f,2.5f),
                targetNPC.position.y,
                targetNPC.position.z + Random.Range(-2.5f, 2.5f)
                );
            if (i % 9 == 0)
            {
                GameObject tmpSmell = Instantiate(smell.gameObject, transform.parent);
                tmpSmell.transform.position = new Vector3(
                     tmpFeather.transform.position.x,
                     tmpFeather.transform.position.y+3.2f,
                     tmpFeather.transform.position.z
                    );
            }

            float dist = Vector3.Distance(targetPlayer.position, targetNPC.position);
            //print("Distance to other: " + dist+"i: "+i);
            if (dist <= 65) break;
            else yield return null;
        }


        //for (int i = 0; i < 32; i += 2)
        //{
        //    float step = i * x_mod;
        //    graphPoints.Add(new Vector2(step, Mathf.Sin(step) * y_mod));
        //}
        //for(int i=0; i<graphPoints.Count; i++)
        //{
        //    Debug.Log(graphPoints[i]);
        //}
    }
}
