using System.Collections.Generic;
using UnityEngine;
public class RotationCalculator : ScriptableObject
{
    public float rectRad;
    public float rectRadZ;
    public float yMod;
    public float maxAngleX = 90.0f;
    public float maxAngleZ = 25.0f;

    Vector3 centr;
    Vector3 frontVec;
    Vector3 backVec;
    Vector3 frontRayVec;
    Vector3 backRayVec;

    float yAngle;
    float x, y, z;
    float deltaY; //y diff between frontray & backray
    float alpha; //угол между верхней и нижней стороной трапеции, образуемыми линиями рейкаста 

    RaycastHit hitFront;
    RaycastHit hitBack;

    //управляет осью x
    public void MakeCalculations(Transform transform, Transform modelToRotate)
    {
        yAngle = transform.rotation.eulerAngles.y;
        centr = new Vector3(transform.position.x, transform.position.y + yMod, transform.position.z);
        x = Mathf.Sin(Mathf.Deg2Rad * yAngle) * rectRad;
        y = 0.0f;
        z = Mathf.Cos(Mathf.Deg2Rad * yAngle) * rectRad;
        frontVec = new Vector3(centr.x + x, centr.y + y, centr.z + z);
        backVec = new Vector3(centr.x - x, centr.y - y, centr.z - z);

        if (Physics.Raycast(frontVec, Vector3.down, out hitFront, 25.0f, 1 << 9))
            frontRayVec = hitFront.distance * Vector3.down;
        if (Physics.Raycast(backVec, Vector3.down, out hitBack, 25.0f, 1 << 9))
            backRayVec = hitBack.distance * Vector3.down;

        deltaY = frontRayVec.y - backRayVec.y;

        alpha = -Mathf.Rad2Deg * Mathf.Atan2(deltaY, rectRad * 2.0f);
        if (Mathf.Abs(alpha) < maxAngleX)
            modelToRotate.rotation = Quaternion.Euler(alpha, modelToRotate.rotation.eulerAngles.y, modelToRotate.rotation.eulerAngles.z);
    }

    Vector3 frontVecZ;
    Vector3 backVecZ;
    Vector3 frontRayVecZ;
    Vector3 backRayVecZ;

    public void MakeCalculationsZ(Transform transform, Transform modelToRotate)
    {
        yAngle = transform.rotation.eulerAngles.y;
        centr = new Vector3(transform.position.x, transform.position.y + yMod, transform.position.z);
        x = Mathf.Cos(Mathf.Deg2Rad * yAngle) * rectRadZ;
        y = 0.0f;
        z = Mathf.Sin(Mathf.Deg2Rad * yAngle) * rectRadZ;

        frontVecZ = new Vector3(centr.x + x, centr.y + y, centr.z + z);
        backVecZ = new Vector3(centr.x - x, centr.y - y, centr.z - z);

        if (Physics.Raycast(frontVecZ, Vector3.down, out hitFront, 25.0f, 1 << 9))
            frontRayVecZ = hitFront.distance * Vector3.down;
        if (Physics.Raycast(backVecZ, Vector3.down, out hitBack, 25.0f, 1 << 9))
            backRayVecZ = hitBack.distance * Vector3.down;

        deltaY = frontRayVecZ.y - backRayVecZ.y;

        alpha = Mathf.Rad2Deg * Mathf.Atan2(deltaY, rectRadZ * 2.0f);
        if (Mathf.Abs(alpha) < maxAngleZ)
            modelToRotate.rotation = Quaternion.Euler(modelToRotate.rotation.eulerAngles.x, modelToRotate.rotation.eulerAngles.y, alpha);
    }

    public void DebugLines()
    {
        Debug.DrawLine(centr, frontVec, Color.magenta);
        Debug.DrawLine(centr, backVec, Color.magenta);
        Debug.DrawRay(frontVec, frontRayVec);
        Debug.DrawRay(backVec, backRayVec);

        Debug.DrawLine(centr, frontVecZ, Color.magenta);
        Debug.DrawLine(centr, backVecZ, Color.magenta);
        Debug.DrawRay(frontVecZ, frontRayVecZ);
        Debug.DrawRay(backVecZ, backRayVecZ);
    }
}