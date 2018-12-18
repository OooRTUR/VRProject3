using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPointer : MonoBehaviour {
    [SerializeField] Transform canvasCenter;
    [SerializeField] Transform player;
    [SerializeField] public Transform target3d;
    [SerializeField] FieldOfViewAudio fow;
    [SerializeField] Image image;
    [SerializeField] float rad;

    public static TestPointer instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    // Use this for initialization
    void Start () {
        //StartCoroutine("ControlPointer");
	}
	
	// Update is called once per frame
	void Update () {

        if(fow.isDanger && target3d!=null)
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }

        if (target3d != null)
        {
            float targetAngle = Vec3Mathf.GetAngle1(player.position, target3d.position) + Camera.main.transform.rotation.eulerAngles.y;
            transform.localPosition = Vec3Mathf.GetCirclePointXZ(canvasCenter.localPosition, targetAngle, rad);

            float selfAngle = Vec3Mathf.GetAngleCanvas(transform.localPosition, canvasCenter.localPosition);
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, selfAngle);
        }

        //Debug.DrawLine(canvasCenter.position, new Vector3(canvasCenter.position.x, canvasCenter.position.y + 540.0f, canvasCenter.position.z));
        //Debug.DrawLine(canvasCenter.position, transform.position);

    }

    IEnumerator ControlPointer()
    {
        while (true)
        {
           
            yield return new WaitForSeconds(0.01f);
        }
    }
}
