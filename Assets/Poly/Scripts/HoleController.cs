using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoleController : MonoBehaviour {
    public List<GameObject> mousesInHole = new List<GameObject>();
    public Image filler;
    float fillAmount;
    bool left = true, right;

    private void Update()
    {
        filler.fillAmount = fillAmount;

        if(fillAmount > 0)
            fillAmount -= Time.deltaTime * 0.1f;

        if(fillAmount > 0.95f)
        {
            fillAmount = 0;
            mousesInHole[0].GetComponent<CatchAnimal>().Catch();
            mousesInHole.Remove(mousesInHole[0]);
        }
    }

    public void TryTakeMouse ()
    {
        if (mousesInHole.Count > 0)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && left)
            {
                left = false;
                right = true;
                fillAmount += 0.05f;
            }

            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && right)
            {
                right = false;
                left = true;
                fillAmount += 0.05f;
            }
        }
    }
}
