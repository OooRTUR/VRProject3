using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmellsController : MonoBehaviour {

    public SmellController[] smells;
    public MouseLocationSound[] sounds;
    public Transform start;
    public Transform rabbit;
    public int index;
    public int sIndex;
    public float rad;
    public float dim;
    public float rar;
    public GameObject pref;
    public Transform smellsParent;
    public float corr;



    PlaceObjsByCurve place;
    NavMeshPath path;
    Vector3[] pathCorners;
	// Use this for initialization
	public void Run () {
        
        //GeneratePath();
        //InitSmells();
        //StartCoroutine(DrawPath());
	}

    public void GeneratePath()
    {
        path = new NavMeshPath();
        NavMesh.CalculatePath(start.position, rabbit.position, 1<<4, path);
        pathCorners = new Vector3[path.corners.Length];
        pathCorners = path.corners;

        place = ScriptableObject.CreateInstance<PlaceObjsByCurve>();
        place.rad = rad;
        place.dim = dim;
        place.rar = 8;
        place.offset = 10.0f;

        float uplift = 5.0f;
        for (int i = 0; i < pathCorners.Length - 1; i++)
        {
            Vector3 nullY1 = new Vector3(pathCorners[i].x, 0.0f + uplift, pathCorners[i].z);
            Vector3 nullY2 = new Vector3(pathCorners[i + 1].x, 0.0f + uplift, pathCorners[i + 1].z);
            place.Run(
                nullY1,
                nullY2);
            place.objsParent = smellsParent;
            place.corr = corr;
            //Debug.Log(pref.name);
            place.PlaceObjByGraph(pref);
        }
    }



    public void InitSmells()
    {
        Debug.Log(transform.name);
        smells = new SmellController[smellsParent.childCount];
        for(int i=0; i <smellsParent.childCount; i++)
        {
            smells[i] =  smellsParent.GetChild(i).GetComponent<SmellController>();
            smells[i].rabbit = rabbit;
            
        }

        for (int i = 0; i < smells.Length; i++)
        {
            smells[i].OnEnter += delegate (GameObject obj)
            {
                if (index < smells.Length - 4)
                {
                    index++;
                    smells[index+3].gameObject.SetActive(true);
                }
            };
            smells[i].gameObject.SetActive(false);
        }
        index = 0;
        smells[index].gameObject.SetActive(true);
        smells[index + 1].gameObject.SetActive(true);
        smells[index + 2].gameObject.SetActive(true);
    }


   public void InitMouseSounds()
    {
        //Debug.Log(transform.name);
        sounds = new MouseLocationSound[smellsParent.childCount];
        for (int i = 0; i < smellsParent.childCount; i++)
        {
            sounds[i] = smellsParent.GetChild(i).GetComponent<MouseLocationSound>();
            sounds[i].rabbit = rabbit;
        }
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].OnEnter += delegate (GameObject obj)
            {
                if (index < sounds.Length - 4)
                {
                    index++;
                    sounds[index].gameObject.SetActive(true);
                }
            };
            sounds[i].gameObject.SetActive(false);
        }
        sIndex = 0;
        sounds[index].gameObject.SetActive(true);
        sounds[index + 1].gameObject.SetActive(true);
        sounds[index + 2].gameObject.SetActive(true);
    }

    IEnumerator DrawPath()
    {
        while (true)
        {
            for(int i=0; i<pathCorners.Length; i++)
            {
                Vector3 tmpCorn = pathCorners[i];
                Debug.DrawLine(tmpCorn, new Vector3(tmpCorn.x, tmpCorn.y+2.0f, tmpCorn.z),Color.red);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
