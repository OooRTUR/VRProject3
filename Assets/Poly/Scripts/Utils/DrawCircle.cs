using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : ScriptableObject
{
    public int segments = 50;
    public float xradius = 1;
    public float yradius = 1;
    LineRenderer line;

   public DrawCircle(LineRenderer lineRenderer)
    {
        line = lineRenderer;
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
    }

    public void Draw()
    {
        float x;
        float y=0.0f;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}
