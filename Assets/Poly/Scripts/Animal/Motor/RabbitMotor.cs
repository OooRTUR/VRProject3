using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RabbitMotor : AnimalMotor
{

	protected override void Update ()
	{
		animator.SetFloat ("Speed", agent.velocity.magnitude);
	}

    protected override IEnumerator Alarm()
    {
        float time = 0;
        float chaseTime = 0;
        //Debug.Log("overrided Alarm() started | this is inherited method from AnimalMotor");
		agent.speed = runSpeed;
        float randomSec = Random.Range(0.5f, 0.6f);
        //agent.ResetPath();
        agent.SetDestination(Vec3Mathf.GetReverseDir(transform.position, visibleTarget.position, 35));
		while (true)
        {
            time += Time.deltaTime;
            chaseTime += Time.deltaTime;
            //Debug.Log(chaseTime);
            //Debug.Log(time);
            if (time >= randomSec)
            {
                float randomAngle = Random.Range(-fow.viewAngle/2, fow.viewAngle/2);
                //Debug.Log("Задаем новою точку перемещения");
                //agent.ResetPath();
                agent.SetDestination(Vec3Mathf.GetReverseDir(transform.position, visibleTarget.position, 15, randomAngle));
                time = 0;
                //randomSec = Random.Range(0.3f, 0.6f);
            }
			if (fow.visibleTargets.Count == 0 && chaseTime > 1.5f)
				break;
            yield return new WaitForSeconds(0.05f);
        }
        //Debug.Log("changing condition to alarm");
        visibleTarget = null;
        ChangeCondition(Condition.Secure, "Alarm", "Secure");
    }
}