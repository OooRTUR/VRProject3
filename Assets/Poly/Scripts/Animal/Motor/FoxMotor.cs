using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxMotor : AnimalMotor {

	float attackDistance = 8;
	[SerializeField] Renderer model;

	protected override void Awake ()
	{
		agent = GetComponent<NavMeshAgent>();
		ai = GetComponent<AnimalAI>();
		animator = GetComponent<Animator> ();
	}

	protected override void Start ()
	{
		ai.FindAreaCenter ();
		fow = ai.zonesManager.GetComponent<FieldOfView> ();
		agent.Warp (ai.areaCenter.position);
		StartCoroutine ("Secure");
	}

	protected override void Update ()
	{
		animator.SetFloat ("Speed", agent.velocity.magnitude);
	}

	protected override IEnumerator Secure ()
	{
		while (fow.visibleTargets.Count < 1) {
			yield return null;
			if(Vec3Mathf.DistanceTo (transform.position, ai.areaCenter.position) < 1.3f)
				model.enabled = false;
		}
		visibleTarget = fow.visibleTargets [0];
		yield return new WaitForSeconds (5);
		if (fow.visibleTargets.Count > 0)
			ChangeCondition (Condition.Alarm, "Secure", "Alarm");
		else
			StartCoroutine ("Secure");
	}

	protected override IEnumerator Alarm()
	{
		if (!model.enabled) {
			ai.FindAreaCenter (visibleTarget.position);
			agent.Warp (ai.areaCenter.position);
			model.enabled = true;
		}
		agent.speed = runSpeed;
		while (fow.visibleTargets.Count > 0) {
			float dist = Vec3Mathf.DistanceTo (transform.position, visibleTarget.position);
			if (dist < attackDistance) {
				PlayerHealth playerHP = FindObjectOfType<PlayerHealth> ();
				if(playerHP!=null)
					playerHP.TakeDamage (30);
				agent.ResetPath ();
				yield return new WaitForSeconds (1f);
			}
			agent.SetDestination(destination =  visibleTarget.position);
			yield return new WaitForSeconds(0.1f);
		}
		agent.ResetPath();
		ChangeCondition(Condition.Safety, "Alarm", "Safety");
	}

	protected override IEnumerator Safety ()
	{
		agent.speed = walkSpeed;
		ai.FindAreaCenter ();
		yield return new WaitForSeconds (3);
		agent.SetDestination (ai.areaCenter.position);
		ChangeCondition (Condition.Secure, "Safety", "Secure");
	}
}
