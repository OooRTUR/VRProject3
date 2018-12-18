using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrailsZone : MonoBehaviour
{

    public enum TrailType { trail, smell, sound };

    public float widthZone;
    public float lengthZone;
    public float rad;
    public float dim;
    public float corr;
    public GameObject setTrailsPrefab;
    public GameObject setSmellPrefab;
    public GameObject setSoundPrefab;

    public GameObject trailPrefab;
    public GameObject smellPrefab;
    public GameObject soundPrefab;

    Bounds bounds;
    //LineRenderer line;

    //void Awake () {
    //	Vector3 size = new Vector3 (widthZone, 0, lengthZone);
    //	bounds = new Bounds (transform.position, size);
    //	line = GetComponent<LineRenderer> ();
    //	line.positionCount = 5;
    //	line.SetPosition (0, new Vector3 (bounds.min.x, bounds.center.y, bounds.min.z));
    //	line.SetPosition (1, new Vector3 (bounds.min.x, bounds.center.y, bounds.max.z));
    //	line.SetPosition (2, new Vector3 (bounds.max.x, bounds.center.y, bounds.max.z));
    //	line.SetPosition (3, new Vector3 (bounds.max.x, bounds.center.y, bounds.min.z));
    //	line.SetPosition (4, new Vector3 (bounds.min.x, bounds.center.y, bounds.min.z));
    //	//posFrom = posTo;
    //	//posTo.x = bounds.max.x;
    //	//posTo.z = bounds.max.z;
    //	//Debug.DrawLine (posFrom, posTo);
    //}

    public void InitTrail(string tag, TrailType trailType)
    {
        //float x = Random.Range (bounds.min.x, bounds.max.x);
        //float z = Random.Range (bounds.min.z, bounds.max.z);
        //Vector3 trailPos = new Vector3 (x, bounds.center.y, z);

        if (trailType == TrailType.trail)
        {
            Vector3 trailPos = transform.position;
            GameObject trail = Instantiate(setTrailsPrefab, trailPos, Quaternion.identity, transform);
            SetTrails st = trail.GetComponent<SetTrails>();
            st.name = "trail";
            st.startTransform = trail.transform;
            st.endTransform = GameObject.FindGameObjectWithTag(tag).transform;
            st.trail = trailPrefab;
            st.corr = corr;
            st.rad = rad;
            st.dim = dim;
            st.Run();
        }
        else if (trailType == TrailType.smell)
        {
            Vector3 smellPos = transform.position;
            GameObject smell = Instantiate(setSmellPrefab, smellPos, Quaternion.identity, transform);
            SmellsController sc = smell.GetComponent<SmellsController>();
            sc.name = "smell";
            sc.start = smell.transform;
            sc.rabbit = GameObject.FindGameObjectWithTag(tag).transform;
            sc.corr = corr;
            sc.rad = rad;
            sc.dim = dim;
            sc.pref = smellPrefab;
            sc.GeneratePath();
            sc.InitSmells();
        }else if (trailType == TrailType.sound)
        {
            Vector3 smellPos = transform.position;
            GameObject sound = Instantiate(setSoundPrefab, smellPos, Quaternion.identity, transform);
            SmellsController sc = sound.GetComponent<SmellsController>();
            sc.name = "sound";
            sc.start = sound.transform;
            sc.rabbit = GameObject.FindGameObjectWithTag(tag).transform;
            sc.corr = corr;
            sc.rad = rad;
            sc.dim = dim;
            sc.pref = soundPrefab;
            sc.GeneratePath();
            sc.InitMouseSounds();
        }
    }
}
