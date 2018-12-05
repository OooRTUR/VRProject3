using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetCirclePos : MonoBehaviour {
    [SerializeField]Transform movableObject;

    [SerializeField]float rad = 10.0f;
    [SerializeField] float modCX;
    [SerializeField] float modCY;
    [SerializeField] float speed = 25.0f;
    Vector3 vec3Zero;
    float alpha = 0.0f;
    List<Vector3> traectory;
	// Use this for initialization
	void Start () {
        traectory = new List<Vector3>();
        traectory.Add(GetCircleSinPoint(alpha, rad, modCY));
        dest = Vec3Mathf.GetCircleSinPoint(vec3Zero, alpha, rad, modCY);
    }

    float timer = 0.0f;
    // Update is called once per frame
    Vector3 dest;
	void Update () {
        
        vec3Zero = transform.position;
        Debug.DrawLine(vec3Zero, Vec3Mathf.GetCircleSinPoint(vec3Zero, alpha,rad,modCY));
        
        movableObject.position = Vector3.MoveTowards(movableObject.position,dest,1.0f);
        timer += Time.deltaTime;
        alpha += 1.0f * Time.deltaTime * speed;
        if (timer > 0.1f)
        {
            timer = 0.0f;
            
            traectory.Add(Vec3Mathf.GetCircleSinPoint(vec3Zero, alpha, rad, modCY));
            dest = Vec3Mathf.GetCircleSinPoint(vec3Zero, alpha, rad, modCY);
        }

        for(int i=1; i < traectory.Count; i++)
        {
            Debug.DrawLine(traectory[i-1], traectory[i]);
        }

	}

    Vector3 GetCircleSinPoint(float angle, float rad, float modCY)
    {
        float x = Mathf.Cos((angle + 90.0f) * Mathf.Deg2Rad) * rad + vec3Zero.x;
        float z = Mathf.Sin((angle + 90.0f) * Mathf.Deg2Rad) * rad + vec3Zero.z;
        float l = Mathf.PI * rad * angle / 180.0f;
        float y = Mathf.Sin(l*0.1f) * modCY;

        return new Vector3(x,y,z);
    }
}
