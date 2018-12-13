using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Vec3Mathf
{
    static public Vector3 DirectionTo(Vector3 point1,Vector3 point2)
    {
        Vector3 direction = (point2 - point1).normalized;
        return direction;
    }

    static public float DistanceTo(Vector3 point1, Vector3 point2)
    {
        float distance = Vector3.Distance(point1, point2);
        return distance;
    }

    static public float GetAngle(Transform start, Transform end)
    {
        Vector3 targetDir = end.position - start.position;
        float angle = Vector3.Angle(targetDir, start.forward);
        if (!(Vector3.Angle(start.right, end.position - start.position) > 90f))
            angle = 360.0f - angle;
        return angle;
    }
    static public float GetAngle(Vector3 start, Vector3 end)
    {
        Vector3 targetDir =  end - start;
        float angle = Vector3.Angle(targetDir, Vector3.forward);
        if (!(Vector3.Angle(Vector3.right, end - start) > 90f))
            angle = 360.0f - angle;
        return angle;
    }
    static public float GetAngle(Vector3 start, Vector3 end, Vector3 axisVec)
    {
        Vector3 targetDir = end - start;
        float angle = Vector3.Angle(targetDir, Vector3.forward);
        if (!(Vector3.Angle(axisVec, end - start) > 90f))
            angle = 360.0f - angle;
        return angle;
    }
    /// <summary>
    /// берет угол строго вдоль оси Y (независимо от положения в x и z). Хороший метод
    /// </summary>
    static public float GetAngle1(Vector3 start, Vector3 end)
    {
        Vector3 targetDir = new Vector3(end.x-start.x, 0.0f, end.z-start.z);
        float angle = Vector3.Angle(targetDir, Vector3.forward);
        if (!(Vector3.Angle(Vector3.right, end - start) > 90f))
            angle = 360.0f - angle;
        return angle;
    }
    static public float GetAngleZY(Vector3 start, Vector3 end)
    {
        Vector3 targetDir = new Vector3(0.0f, end.y-start.y, end.z - start.z);
        float angle = Vector3.Angle(targetDir, Vector3.forward);
        if (!(Vector3.Angle(Vector3.up, end - start) > 90f))
            angle = 360.0f - angle;
        return angle;
    }
    static public float GetAngleXY(Vector3 start, Vector3 end)
    {
        Vector3 targetDir = new Vector3(0.0f, end.y - start.y, end.z - start.z);
        float angle = Vector3.Angle(targetDir, Vector3.forward);
        if (!(Vector3.Angle(Vector3.up, end - start) > 90f))
            angle = 360.0f - angle;
        return angle;
    }
    static public float GetAngleCanvas(Vector3 start, Vector3 end)
    {
        Vector3 targetDir = new Vector3(end.x - start.x, end.y - start.y, 0.0f);
        float angle = Vector3.Angle(targetDir, Vector3.up);
        if (!(Vector3.Angle(Vector3.right, end - start) > 90f))
            angle = 360.0f - angle;
        return angle;
    }
    static public Quaternion GetDir(Vector3 start, Vector3 end)
    {
        float angle = Vec3Mathf.GetAngle1(start, end);
        return Quaternion.Euler(0, -angle, 0); ; // Крутим
    }
    static public Quaternion GetDirX(Vector3 start, Vector3 end)
    {
        float angle = GetAngleXY(start, end);
        angle = (angle > 90.0f && angle < 270.0f) ? 180.0f - angle : angle;
        //Debug.Log(angle);
        return Quaternion.Euler(angle, 0, 0);
    }
    static public Quaternion GetDirXY(Vector3 start, Vector3 end)
    {
        float angle = Vec3Mathf.GetAngle1(start, end);
        float angleX = GetAngleXY(start, end);
        float angleZ = 0.0f;
        if (angle > 90.0f && angle < 270.0f) {
            angle = -(180.0f - angle);
            angleZ = 180.0f;
        }
        //Debug.Log(angle + " | " + angleX);
        return Quaternion.Euler(angleX, -angle, angleZ);
    }

    static public Vector3 GetReverseDir(Vector3 start, Vector3 end, float length=1)
    {
        float angle = Vec3Mathf.GetAngle(end, start);
        float dirX = start.x + Mathf.Cos((angle + 90.0f) * Mathf.Deg2Rad) * length;
        float dirZ = start.z + Mathf.Sin((angle + 90.0f) * Mathf.Deg2Rad) * length;
        return new Vector3(dirX, start.y, dirZ);
    }
    static public Vector3 GetReverseDir(Vector3 start, Vector3 end, float length = 1, float alpha = 15)
    {
        float angle = Vec3Mathf.GetAngle(end, start);
        float dirX = start.x + Mathf.Cos((angle + 90.0f + alpha) * Mathf.Deg2Rad) * length;
        float dirZ = start.z + Mathf.Sin((angle + 90.0f + alpha) * Mathf.Deg2Rad) * length;
        return new Vector3(dirX, start.y, dirZ);
    }
    static public Vector3 GetRotToPoint(Vector3 start, Vector3 end)
    {
        float angle = GetAngle(start, end);
        float dirX = start.x + Mathf.Cos((angle + 90.0f) * Mathf.Deg2Rad);
        float dirZ = start.z + Mathf.Sin((angle + 90.0f) * Mathf.Deg2Rad);
        return new Vector3(dirX, 0.0f, dirZ);
    }

    static public Vector3 GetCircleSinPoint(Vector3 vec3Zero, float angle, float rad, float modCY)
    {
        float x = Mathf.Cos((angle + 90.0f) * Mathf.Deg2Rad) * rad + vec3Zero.x;
        float z = Mathf.Sin((angle + 90.0f) * Mathf.Deg2Rad) * rad + vec3Zero.z;
        float l = Mathf.PI * rad * angle / 180.0f;
        float y = Mathf.Sin(l * 0.1f) * modCY + vec3Zero.y;

        return new Vector3(x, y, z);
    }
    static public Vector3 GetCirclePoint(Vector3 vec3Zero, float angle, float rad)
    {
        float x = Mathf.Cos((angle + 90.0f) * Mathf.Deg2Rad) * rad + vec3Zero.x;
        float z = Mathf.Sin((angle + 90.0f) * Mathf.Deg2Rad) * rad + vec3Zero.z;

        return new Vector3(x, vec3Zero.y, z);
    }
    /// <summary>
    /// взять точку окружности в в плоскости XY
    /// </summary>
    static public Vector3 GetCirclePointXZ(Vector3 vec3Zero, float angle, float rad)
    {
        float x = Mathf.Cos((angle + 90.0f) * Mathf.Deg2Rad) * rad + vec3Zero.x;
        float y = Mathf.Sin((angle + 90.0f) * Mathf.Deg2Rad) * rad + vec3Zero.y;

        return new Vector3(x, y, vec3Zero.z);
    }

}
