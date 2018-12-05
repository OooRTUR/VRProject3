using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsRandomly : MonoBehaviour {

    //serialized...
    [SerializeField] GameObject objectToPlace;
    [SerializeField] Transform spawnContainer;
    [SerializeField] List<GameObject> spawns;
    //not serialized...
    private void Awake()
    {
        //SetSpawnList();
        if (spawnContainer == null) spawnContainer = transform;
        MathFs.SetList(ref spawns, spawnContainer);
    }
    void Start () {
        PlaceObjects();
    }
    void PlaceObjects()
    {
        for(int i=0; i<spawns.Count; i++)
        {
            int isPlace = Random.Range(0, 2);
            //Debug.Log(isPlace);
            if (isPlace == 1)
            {
                GameObject tempGameObj = Instantiate(objectToPlace, spawns[i].transform, false);
                Vector3 position = spawns[i].transform.position;
                tempGameObj.transform.position = position;
            }
        }
    }
    void SetSpawnList()
    {
        if (spawnContainer == null) spawnContainer = transform;
        for (int i = 0; i < spawnContainer.childCount; i++)
        {
            spawns.Add(spawnContainer.GetChild(i).gameObject);
        }
    }
}
