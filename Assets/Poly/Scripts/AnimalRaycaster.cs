using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRaycaster : MonoBehaviour {

    public Transform neck;
    public LayerMask animalMask;
    CatchAnimal animal;

    RaycastHit hit;

    private void Update()
    {
        Ray ray = new Ray(neck.position, neck.forward);
        if (Physics.SphereCast(ray, 2, out hit,4, animalMask))
        {
            if (animal == null || hit.collider.GetComponent<CatchAnimal>().Equals(animal))
            {
                animal = hit.collider.GetComponent<CatchAnimal>();
                if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                    animal.Catch();
                else
                    animal.ResetFill();
            }
        }
        else
        {
            if (animal != null)
            {
                animal.ResetFill();
                animal = null;
            }
        }
    }
}
