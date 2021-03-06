using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public class Animal
{
    public enum AnimalType {Chicken, Rabbit, Mouse, Bear, Eagle, Fox }
    public AnimalType animalType;
    public int count;
    public GameObject prefab;
}
[System.Serializable]
public class TrailsPrefabs
{
    public GameObject chickenTrail;
    public GameObject bearTrail;
    public GameObject eagleTrail;
    public GameObject rabbitSmell;
    public GameObject foxSmell;
    public GameObject mouseSound;
}
[System.Serializable]
public struct AnimalHouses
{
    public GameObject mouseHole;
    public GameObject foxHole;
}
public class SpawnController : MonoBehaviour
{
    public Transform handler;
    public Animal[] animals;

    Transform spawnPointsParent;
    Transform[] spawnPoints;
    ZonesManager[] zonesManagers;
    [SerializeField] GameObject bearPrefab;
    [SerializeField] int bearChildCount;

    [SerializeField]TrailsPrefabs trailPrefabs;
    [SerializeField] AnimalHouses animalHouses;



    private void Awake()
    {
        spawnPointsParent = transform;
        //spawnPoints = new Transform[spawnPointsParent.childCount];
        zonesManagers = new ZonesManager[spawnPointsParent.childCount];
        for (int i = 0; i < zonesManagers.Length; i++)
        {
            zonesManagers[i] = spawnPointsParent.GetChild(i).GetComponent<ZonesManager>();
        }
        zonesManagers = MathFs.RandomizeArray(zonesManagers);
    }
    int spawnIndex = 0;
    void Start()
    {
        
        for(int i=0; i<animals.Length; i++) {
            zonesManagers[i].gameObject.layer = 18;
            switch (animals[i].animalType)
            {
                case Animal.AnimalType.Chicken:
                    zonesManagers[i].transform.tag = WayPointsTags.chicken;
                    zonesManagers[i].transform.name = WayPointsTags.chicken;

                    zonesManagers[i].trailStart.trailPrefab = trailPrefabs.chickenTrail;
                    zonesManagers[i].trailStart.transform.name = WayPointsTags.chicken + "TStart";
                    zonesManagers[i].trailStart.rad = 12.0f;
                    zonesManagers[i].trailStart.dim = 3.0f;
                    zonesManagers[i].trailStart.corr = 0.05f;
                    zonesManagers[i].trailStart.InitTrail(WayPointsTags.chicken, SetTrailsZone.TrailType.trail);
                    break;
                case Animal.AnimalType.Rabbit:
                    zonesManagers[i].transform.tag = WayPointsTags.rabbit;
                    zonesManagers[i].transform.name = WayPointsTags.rabbit;

                    zonesManagers[i].trailStart.smellPrefab = trailPrefabs.rabbitSmell;
                    zonesManagers[i].trailStart.transform.name = WayPointsTags.rabbit + "SStart";
                    zonesManagers[i].trailStart.rad = 28.0f;
                    zonesManagers[i].trailStart.dim = 3.0f;
                    zonesManagers[i].trailStart.InitTrail(WayPointsTags.rabbit, SetTrailsZone.TrailType.smell);
                    break;
                case Animal.AnimalType.Mouse:
                    zonesManagers[i].transform.tag = WayPointsTags.mouse;
                    zonesManagers[i].transform.name = WayPointsTags.mouse;
                    SetAnimalHole(zonesManagers[i], animalHouses.mouseHole);


                    zonesManagers[i].trailStart.soundPrefab = trailPrefabs.mouseSound;
                    zonesManagers[i].trailStart.transform.name = WayPointsTags.mouse + "TStart";
                    zonesManagers[i].trailStart.corr = 4.0f;
                    zonesManagers[i].trailStart.rad = 30.0f;
                    zonesManagers[i].trailStart.dim = 2.0f;
                    zonesManagers[i].trailStart.InitTrail(WayPointsTags.mouse, SetTrailsZone.TrailType.sound);


                    break;
                case Animal.AnimalType.Bear:
                    zonesManagers[i].transform.tag = WayPointsTags.bear;
                    zonesManagers[i].transform.name = WayPointsTags.bear;

                    zonesManagers[i].trailStart.trailPrefab = trailPrefabs.bearTrail;
                    zonesManagers[i].trailStart.transform.name = WayPointsTags.bear + "TStart";
                    zonesManagers[i].trailStart.corr = +0.02f;
                    zonesManagers[i].trailStart.rad = 6.0f;
                    zonesManagers[i].trailStart.dim = 3.0f;
                    zonesManagers[i].trailStart.InitTrail(WayPointsTags.bear, SetTrailsZone.TrailType.trail);
                    InstBearChild(i);
                    break;
                case Animal.AnimalType.Eagle:
                    zonesManagers[i].transform.tag = WayPointsTags.eagle;
                    zonesManagers[i].transform.name = WayPointsTags.eagle;
                    zonesManagers[i].eaglePatrolPoint.tag = "EaglePatrolPoint";

                    zonesManagers[i].trailStart.trailPrefab = trailPrefabs.eagleTrail;
                    zonesManagers[i].trailStart.transform.name = WayPointsTags.eagle + "TStart";
                    zonesManagers[i].trailStart.rad = 12.0f;
                    zonesManagers[i].trailStart.dim = 3.0f;
                    zonesManagers[i].trailStart.corr = 0.05f;
                    zonesManagers[i].trailStart.InitTrail(WayPointsTags.eagle, SetTrailsZone.TrailType.trail);

                    Vector3 tmpPos = zonesManagers[i].transform.position;
                    FieldOfView fow = zonesManagers[i].gameObject.AddComponent<FieldOfView>();
                    fow.checkObstacles = false;
                    fow.viewRadius = 50.0f;
                    fow.viewAngle = 360.0f;
                    fow.targetMask = 1 << 8;
                    fow.obstacleMask = 1 << 16;
                    break;
                case Animal.AnimalType.Fox:
                    zonesManagers [i].transform.tag = WayPointsTags.fox;
					zonesManagers [i].transform.name = WayPointsTags.fox;
					fow = zonesManagers [i].gameObject.AddComponent<FieldOfView> ();
					fow.checkObstacles = false;
					fow.viewRadius = 80;
					fow.viewAngle = 360;
					fow.targetMask = 1 << 8;
					fow.obstacleMask = 1 << 16;
                    SetAnimalHole(zonesManagers[i], animalHouses.foxHole);

                    
                    zonesManagers[i].trailStart.smellPrefab = trailPrefabs.foxSmell;
                    zonesManagers[i].trailStart.transform.name = WayPointsTags.fox + "SStart";
                    zonesManagers[i].trailStart.rad = 28.0f;
                    zonesManagers[i].trailStart.dim = 3.0f;
                    zonesManagers[i].trailStart.InitTrail(WayPointsTags.fox, SetTrailsZone.TrailType.smell);
                    
                    break;
                default:
                    break;
            }

            // Instantiate animals
            for(int j=0; j < animals[i].count; j++)
            {
                InstAnimal(i);
            }
            spawnIndex++;
            if (spawnIndex >= zonesManagers.Length)
                spawnIndex = 0;
        }
    }

    void InstAnimal(int num)
    {
        GameObject tmpgo = Instantiate(animals[num].prefab, zonesManagers[spawnIndex].transform.position, Quaternion.identity, handler);
        NavMeshAgent agent = tmpgo.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
            tmpgo.transform.position = zonesManagers[num].transform.position;
            agent.enabled = true;
        }
    }
    void InstBearChild(int num)
    {
        for (int i = 0; i < bearChildCount; i++)
        {
            GameObject tmpgo = Instantiate(bearPrefab, zonesManagers[spawnIndex].transform.position, Quaternion.identity, handler);
            NavMeshAgent agent = tmpgo.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
                tmpgo.transform.position = zonesManagers[num].transform.position;
                agent.enabled = true;
            }
        }
    }

    // Instantiate hole prefab for animal in saveZones positions
    void SetAnimalHole(ZonesManager zm, GameObject prefab)
    {
        for(int i=0; i< zm.saveZones.Length; i++)
        {
            Instantiate(prefab, zm.saveZones[i], false);
        }
    }
}
