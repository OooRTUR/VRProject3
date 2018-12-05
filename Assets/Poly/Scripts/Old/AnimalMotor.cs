using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace old
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AnimalMotor : MonoBehaviour {

        //public float walkSpeed = 3f;
        //public float runSpeed = 7f;

        //NavMeshAgent agent;
        //AreaOfWalk walkArea;
        //FieldOfView fow;
        //AnimalType a_type;
        //public enum Condition {Walk, Run, Hidden, Fly}
        //public Condition cond;

        //void Awake () {
        //	agent = GetComponent<NavMeshAgent>();
        //	walkArea = GetComponent<AreaOfWalk>();
        //	fow = GetComponent<FieldOfView> ();
        //	a_type = GetComponent<AnimalType> ();
        //}

        //void Update () {
        //	CheckPointDestination ();
        //}

        //void Start () {
        //	Invoke ("StartWalk", 2);
        //}

        //void StartWalk () {
        //       Debug.Log("start walking");
        //	StartCoroutine ("Walking");
        //}

        //public void SawPredator (Vector3 runTo) {
        //	cond = Condition.Run;
        //	StopCoroutine("Walking");
        //	agent.ResetPath ();
        //	Move(runTo);
        //}

        //void Move (Vector3 waypoint) {
        //	agent.speed = cond == Condition.Walk ? walkSpeed : runSpeed;
        //	agent.SetDestination(waypoint);

        //}

        //void CheckPointDestination() {
        //	if (cond == Condition.Run && agent.remainingDistance < 0.5f) {
        //		if (a_type.type == AnimalType.Animal.Mouse) {
        //			agent.Warp (agent.pathEndPosition);
        //			agent.ResetPath ();
        //			cond = Condition.Hidden;
        //			StartCoroutine ("Hidding");
        //		}
        //		if (a_type.type == AnimalType.Animal.Rabbit) {
        //			cond = Condition.Walk;
        //			StartCoroutine ("Walking");
        //		}
        //           if(a_type.type == AnimalType.Animal.Chicken)
        //           {
        //               cond = Condition.Walk;
        //               StartCoroutine("Walking");
        //           }
        //	}
        //}

        //IEnumerator Walking () {
        //	transform.localScale = Vector3.one;
        //	while(cond == Condition.Walk) {
        //		Move(walkArea.GetWalkPoint());
        //		float randomSec = Random.Range(2.5f,6.5f);
        //		yield return new WaitForSeconds(randomSec);
        //	}
        //}

        //IEnumerator Hidding () {
        //	transform.localScale = Vector3.one * 0.2f;
        //	yield return new WaitForSeconds (5);
        //	while (true) {
        //		if (fow.visibleTargets.Count > 0)
        //			yield return new WaitForSeconds (15);
        //		else
        //			break;
        //	}
        //	cond = Condition.Walk;
        //	StartCoroutine ("Walking");
        //}
    }
}