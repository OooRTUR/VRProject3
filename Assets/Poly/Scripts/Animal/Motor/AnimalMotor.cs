using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

// базовые состояния для всех AnimalMotor InSecurity (безопасность) и InAlarm (угроза),
// если поведение объекта отличается от InAlarm (например объект вместо бегства объект пытается скрыться),
// то достаточно переписать метод InAlarm в производном классе

public class AnimalMotor : MonoBehaviour
{
	[SerializeField]protected float walkSpeed = 3f;
	[SerializeField]protected float runSpeed = 12f;

    protected NavMeshAgent agent;
    protected FieldOfView fow;
    protected AnimalAI ai;
    protected Transform visibleTarget;
    protected Vector3 destination;
	protected Animator animator;


    public enum Condition { Secure, Alarm, Safety}
    [SerializeField] public Condition cond;


    //protected AnimalType animalType;
    [HideInInspector] public Transform[] saveZones;
    
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        fow = GetComponent<FieldOfView>();
        ai = GetComponent<AnimalAI>();
		animator = GetComponent<Animator> ();    
    }

    protected virtual void Start()
    {
		ai.FindAreaCenter();
        StartCoroutine("Secure");
    }

	protected virtual void Update () {}

    // состояние безопасности, нормальная скорость перемещения
    protected virtual IEnumerator Secure()
    {
        //FindObjectOfType<PlayerAudioController>().isDanger = false;
        float time = 0.0f;
        //Debug.Log("base Secure() started | this is base method from AnimalMotor");
        agent.speed = walkSpeed;
        float randomSec = Random.Range(1.5f, 3.5f);
        agent.SetDestination(destination = ai.GetWalkPoint());
        while (cond == Condition.Secure)
        {
            time += Time.deltaTime;
            if (time >= randomSec)
            {
                agent.SetDestination(destination = ai.GetWalkPoint());
                time = 0.0f;
            }
            if (fow.visibleTargets.Count > 0)
                break;
            yield return new WaitForSeconds(0.1f);
        }
        visibleTarget = fow.visibleTargets[0];
        //FindObjectOfType<PlayerAudioController>().isDanger = true;
        ChangeCondition(Condition.Alarm, "Secure", "Alarm");
    }

    // состояние опасности
    protected virtual IEnumerator Alarm()
    {
        yield return null;    
    }

    // состояние защиты
    protected virtual IEnumerator Safety()
    {
        yield return null;
    }

    protected virtual void ChangeCondition(Condition targetCond, string currentCoroutine, string targetCoroutine)
    {
        this.cond = targetCond;
        StartCoroutine(targetCoroutine);
        StopCoroutine(currentCoroutine);
    }

    protected void DebugDestination()
    {
        if (destination != null)
        {
            Debug.DrawLine(destination, new Vector3(destination.x, destination.y + 50.0f, destination.z), Color.red);
        }
    }

    public IEnumerator MoveDelay(float divide)
    {
        float speed = cond == Condition.Alarm ? runSpeed : walkSpeed;
        float delay = divide / agent.velocity.magnitude;
        agent.speed = 0;
        yield return new WaitForSeconds(delay);
        agent.speed = speed;
    }
}

[Serializable]
public struct AnimalType
{
    public enum Animal { Animal, Mouse, Chicken, Rabbit }
    public Animal type;
}