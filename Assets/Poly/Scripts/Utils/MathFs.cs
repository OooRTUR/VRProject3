using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathFs
{
    public static T[] RandomizeArray<T>(T[] items ) {
        for (int i = items.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            // обменять значения data[j] и data[i]
            var temp = items[j];
            items[j] = items[i];
            items[i] = temp;
        }
        return items;
    }

    static public void SetList(ref List<GameObject> objectsList, Transform spawnContainer)
    {
        for (int i = 0; i < spawnContainer.childCount; i++)
        {
            objectsList.Add(spawnContainer.GetChild(i).gameObject);
        }
    }
}
