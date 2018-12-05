using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAngle : MonoBehaviour {

    [SerializeField]Transform starttrans;
    [SerializeField]Transform endtrans;

    Vector3 start;
    Vector3 end;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        start = starttrans.position;
        end = endtrans.position;
        Debug.DrawLine(start, end, Color.cyan);
        Debug.DrawLine(start, new Vector3(start.x+5.0f, start.y, start.z), Color.red);
        Debug.DrawLine(start, new Vector3(start.x, start.y, start.z+5.0f), Color.blue);
        Debug.DrawLine(start, new Vector3(start.x, start.y+5.0f, start.z), Color.green);
        Debug.DrawLine(end, new Vector3(end.x + 5.0f, end.y, end.z), Color.red);
        Debug.DrawLine(end, new Vector3(end.x, end.y, end.z+5.0f), Color.blue);
        Debug.DrawLine(end, new Vector3(end.x, end.y + 5.0f, end.z), Color.green);

        Debug.Log(Vec3Mathf.GetAngle(start, end,Vector3.up));
    }

    //IEnumerator SetAngle() {
    //    while (true)
    //    {
    //        if(end.y != start.y)
    //        {
    //            transform.Rotate()
    //        }
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //}
}
