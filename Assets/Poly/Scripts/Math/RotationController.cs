using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotationController : MonoBehaviour {
    RotationCalculator rotCalc;
    [SerializeField] Transform modelToRotate;
    [SerializeField] bool debugMode;
    [SerializeField] float rectRad;
    [SerializeField] float rectRadZ;
    [SerializeField] float yMod;


    void Start () {
        rotCalc = ScriptableObject.CreateInstance<RotationCalculator>();
        StartCoroutine(ModifyAngle());
	}
    IEnumerator ModifyAngle()
    {
        while (true)
        {
            rotCalc.rectRad = rectRad;
            rotCalc.rectRadZ = rectRadZ;
            rotCalc.yMod = yMod;
            rotCalc.MakeCalculations(transform, modelToRotate);
            rotCalc.MakeCalculationsZ(transform, modelToRotate);
            if(debugMode) rotCalc.DebugLines();
            yield return new WaitForSeconds(0.01f);
        }
    }
    

    
}
