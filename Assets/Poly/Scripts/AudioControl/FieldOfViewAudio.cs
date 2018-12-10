using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FieldOfViewAudio : MonoBehaviour
{
    public LayerMask targetMask;
    public float viewRadius;
    public bool isDanger;

    private void Start()
    {
        StartCoroutine(FindTargets());
    }
    void FindVisibleTargets()
    {
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        if (targetsInView.Length > 0)
            isDanger = true;
        else
            isDanger = false;
    }

    IEnumerator FindTargets()
    {
        while (true)
        {
            FindVisibleTargets();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
