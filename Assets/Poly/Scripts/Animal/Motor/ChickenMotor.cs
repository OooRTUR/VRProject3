using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;

public class ChickenMotor : AnimalMotor
{
	public string epTag;
	ZonesManager escapePoint;

    protected override void Start()
    {
        base.Start();
        GameObject handler = GameObject.FindGameObjectWithTag(WayPointsTags.chicken);
        escapePoint = handler.GetComponent<ZonesManager>();
    }

    protected override void Update ()
	{
		animator.SetFloat ("Speed", agent.velocity.magnitude);
		animator.SetBool ("Fly", agent.isOnOffMeshLink);
		if (agent.isOnOffMeshLink)
			agent.speed = walkSpeed;
        DebugDestination();
	}

    protected override IEnumerator Alarm()
    {
        float time = 0;
        float escape_time = 0;
        //Debug.Log("overrided Alarm() started | this is inherited method from AnimalMotor");
        agent.speed = runSpeed;
        float randomSec = Random.Range(0.2f, 0.3f);
        //agent.ResetPath();
        agent.SetDestination(destination = Vec3Mathf.GetReverseDir(transform.position, visibleTarget.position, 20.0f));
		while (true)
        {
            time += Time.deltaTime;
            escape_time += Time.deltaTime;
			if (time >= randomSec)
            {
                //Debug.Log("Задаем новою точку перемещения");
                //agent.ResetPath();
                agent.SetDestination(destination = Vec3Mathf.GetReverseDir(transform.position, visibleTarget.position, 20.0f, 15));
                time = 0;
            }
            if (fow.visibleTargets.Count > 0 && escape_time > 1)
                break;
            if(fow.visibleTargets.Count < 1 && escape_time > 1)
            {
                ChangeCondition(Condition.Secure, "Alarm", "Secure");
            }
            yield return new WaitForSeconds(0.05f);
        }
        escape_time = 0;
        //Debug.Log("changing condition to alarm");
        visibleTarget = null;
        ChangeCondition(Condition.Safety, "Alarm", "Safety");
    }
    protected override IEnumerator Safety()
    {
       // Vector3 lastPosition = agent.destination;
        //agent.ResetPath();
        agent.SetDestination(destination =  escapePoint.GetSaveZone(transform.position));
        yield return new WaitForSeconds(0.1f);
        while (Vector3.Distance (transform.position, agent.destination) > 1)
        {
            //Debug.Log(agent.destination+" "+ transform.position);
            yield return new WaitForSeconds(0.1f);
        }
        //Debug.Log("Курица сидит");
        yield return new WaitForSeconds(3);
        //Vector3 savePos = escapePoint.saveZones[0].position;
        //Debug.Log("Курицу забрали.");

        //Vector3 targetDir = savePos - transform.position;
        //float step = flySpeed * Time.deltaTime;
        //while (Vector3.Distance(transform.position, savePos) > 0)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, savePos, flySpeed * Time.deltaTime);
        //    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        //    transform.rotation = Quaternion.LookRotation(newDir);
        //    yield return new WaitForEndOfFrame();
        //}
        //yield return new WaitForSeconds(1.5f);
        //Debug.Log("Курица возвращается на исходную позицию");
        //while (Vector3.Distance(transform.position, lastPosition) > 0)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, lastPosition, flySpeed*Time.deltaTime);
        //    yield return new WaitForEndOfFrame();
        //}
        //agent.enabled = true;
        //Debug.Log("Курица снова гуляет");
        ChangeCondition(Condition.Secure, "Safety", "Secure");
    }
}