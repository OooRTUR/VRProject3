using UnityEngine;

//данный скрип на старте игры помещает объект, к которому приатачен скрипт 
//к объекту расположенному ниже по оси Y

public class PlaceObjectToGround : ScriptableObject {

    //поправка при размещение объекта на земле - чем больше значение, тем дальше объект от земли
    [SerializeField]public float corr = 0.05f;
    Transform transform;
    float maxDist = 55.0f;
    int groundMask = 1 << 16; //9 исходный, 16 в сборке

    public void Place(ref GameObject go)
    {
        transform = go.transform;
        Vector3 dir = Vector3.down;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, maxDist, groundMask))
        {
            //Debug.DrawRay(transform.position, dir * hit.distance, Color.green);
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - hit.distance + corr,
                transform.position.z
                );
        }
    }

    public void Place( Transform go)
    {
        transform = go;
        Vector3 dir = Vector3.down;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, maxDist, groundMask))
        {
            //Debug.DrawRay(transform.position, dir * hit.distance, Color.green);
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - hit.distance + corr,
                transform.position.z
                );
        }
    }

}
