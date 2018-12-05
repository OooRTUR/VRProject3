using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EagleMotor : AnimalMotor
{
    [SerializeField] public Transform attackTarget;
    [SerializeField] float remDist;
    [SerializeField] float rad;
    [SerializeField]float anglePlus;
    [SerializeField]Transform eagleWaitPoint;
    Vector3 eaglePatrolPoint;
    float takeOffSpeed = 10.0f;
    [SerializeField]string currentCond;
    [SerializeField]bool isEnemySpotted;
    [SerializeField]bool isEnemyInSight;
    [SerializeField] bool isInTheSky;
    Vector3 eaglePatrolPos;
    

    [SerializeField]ZonesManager zm;
    LogTraectoryBuilder traectory = new LogTraectoryBuilder();


    private void Update()
    {
        if (destination != null)
        {
            Debug.DrawLine(destination, new Vector3(destination.x, destination.y + 50.0f, destination.z),Color.red);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        runSpeed = 55.0f;
        walkSpeed = 35.0f;
        agent.enabled = false;
    }
    protected override void Start()
    {
        zm = GameObject.FindGameObjectWithTag(WayPointsTags.eagle).GetComponent<ZonesManager>();
        eaglePatrolPoint = zm.eaglePatrolPoint.position;
        eagleWaitPoint = zm.transform;
        transform.position = eagleWaitPoint.position;
        fow = zm.GetComponent<FieldOfView>();
        eaglePatrolPos = GameObject.FindGameObjectWithTag(WayPointsTags.eaglePatrol).transform.position;
        StartCoroutine("Secure");
    }



    //=================================================================================================================================
    //=================================================================================================================================
    protected override IEnumerator Secure()
    {
        //safetyWalkTime = 0.0f;
        //float waitTime = 0.0f;
        //while (true){
        //    waitTime += 1.0f;
        //    if (waitTime >= 4.0f)
        //        break;
        //    if (isEnemySpotted)
        //        break;
        //    yield return new WaitForSeconds(1.0f);
        //}
        yield return new WaitForSeconds(2.0f);
        ChangeCondition(Condition.Secure, "Secure", "FlyToTheSky");
    }
    protected IEnumerator FlyToTheSky()
    {
        
        float speed = 0.1f;
        agent.enabled = false;
        traectory.graphCorrection = 200.0f;
        traectory.CalcGraph(transform.position, destination = eaglePatrolPoint);
        Vector3 nextP = traectory.GetNextPoint();
        transform.rotation = Vec3Mathf.GetDir(transform.position, destination);
        while (Vector3.Distance(transform.position, destination) > 1.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextP, 0.5f);
            
            //speed += Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, destination, speed);
            //Debug.Log(Vector3.Distance(transform.position, destination));
            if (Vector3.Distance(transform.position, nextP) < 0.1f)
                nextP = traectory.GetNextPoint();
            yield return new WaitForEndOfFrame();
        }
        agent.enabled = true;

        //if (isEnemySpotted)
        //    ChangeCondition(Condition.Alarm, "FlyToTheSky", "Alarm");
        //else
            ChangeCondition(Condition.Safety, "FlyToTheSky", "Safety");
    }



    //=================================================================================================================================
    //=================================================================================================================================
    protected override IEnumerator Alarm()
    {
        Debug.Log("Режим патрулирования");
        if (!agent.enabled) agent.enabled = true;
        float condTime = 0.0f;
        float angle = Vec3Mathf.GetAngle(transform.position, agent.destination);
        //Debug.Log(angle);
        destination = Vec3Mathf.GetCirclePoint(eaglePatrolPos, angle, rad);
        agent.SetDestination(destination);
        while (true)
        {
            condTime += 0.01f;//Time.deltaTime;
            if (agent.remainingDistance < remDist)
            {
                //Debug.Log(angle);
                angle += anglePlus;
                destination = Vec3Mathf.GetCirclePoint(eaglePatrolPos, angle, rad);
                agent.SetDestination(destination);
                //Debug.Log("Добавляю угол:" + anglePlus);
            }
            //if (condTime >= 5.0f)
            //{
            //    isEnemySpotted = false;
            //    Debug.Log("Переход в обычное управление полетом");
            //    ChangeCondition(Condition.Safety, "Alarm","Safety");
            //}
            if (fow.visibleTargets.Count == 0)
                ChangeCondition(Condition.Safety, "Alarm", "Safety");
            if (fow.visibleTargets.Count >0 && condTime >= 5.0f)
            {
                ChangeCondition(Condition.Alarm, "Alarm", "MakeDive");
            }
            Debug.DrawLine(eaglePatrolPos, agent.destination, Color.red);
            yield return new WaitForSeconds(0.01f);
        }
    }
    float attackDistance = 4.75f;
    // методы траектории
    IEnumerator MakeDive()
    {
        agent.enabled = false;
        traectory.graphCorrection = 50.0f;
        traectory.CalcGraph(transform.position, visibleTarget.position);
        destination = traectory.GetNextPoint();
        Debug.Log("Совершили атаку на игрока");
        transform.rotation = Vec3Mathf.GetDir(transform.position, visibleTarget.position);
        while (Vector3.Distance(transform.position, visibleTarget.position)>1.0f)
        {
            transform.rotation = Vec3Mathf.GetDirX(transform.position, visibleTarget.position);
            transform.position = Vector3.MoveTowards(transform.position, visibleTarget.position, 1.0f);
            float dist = Vec3Mathf.DistanceTo(transform.position, visibleTarget.position);
            Debug.Log(dist);
            if (dist < attackDistance)
            {
                PlayerHealth playerHP = FindObjectOfType<PlayerHealth>();
                if (playerHP != null)
                    playerHP.TakeDamage(70);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        traectory.graphCorrection = 0.0f;
        traectory.CalcGraph(transform.position, destination = eaglePatrolPos);
        transform.rotation = Vec3Mathf.GetDirX(transform.position, destination);
        while (Vector3.Distance(transform.position, destination) > 1.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, 1.0f);
            yield return new WaitForEndOfFrame();
        }
        isEnemyInSight = false;
        ChangeCondition(Condition.Alarm, "MakeDive", "Alarm");
    }

    //=================================================================================================================================
    //=================================================================================================================================
    float safetyWalkTime = 0.0f;
    protected override IEnumerator Safety()
    {
        float condTime = 0.0f;
        agent.SetDestination(destination);
        while (true)
        {
            safetyWalkTime += 0.1f;
            condTime += 0.1f;//Time.deltaTime;
            if (agent.remainingDistance < 7)
            {
                agent.ResetPath();
                agent.SetDestination(destination = ai.GetNextZone());
                agent.speed = runSpeed;
                //Debug.Log("Получили новую цель перемещения");
            }
            if (condTime >= 10.0f)
            {
                Debug.Log("Перех в FlyAroundPoint");
                ChangeCondition(Condition.Safety, "Safety", "FlyAroundPointSafety");
            }
            if (fow.visibleTargets.Count > 0)
            {
                isEnemySpotted = true;
                visibleTarget = fow.visibleTargets[0];
                ChangeCondition(Condition.Alarm, "Safety", "Alarm");
            }
            //if (safetyWalkTime > 15.0f)
            //{
            //    ChangeCondition(Condition.Secure, "Safety", "GoBackToTheSpot");
            //}
            //Debug.Log(agent.remainingDistance);
            //Debug.Log("Достигли конечно точки");
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FlyAroundPointSafety()
    {
        Vector3 startPos = transform.position;
        float condTime = 0.0f;
        float angle = Vec3Mathf.GetAngle(transform.position, agent.destination);
        //Debug.Log(angle);
        agent.SetDestination(destination = Vec3Mathf.GetCirclePoint(startPos, angle, rad));
        while (true)
        {
            safetyWalkTime += 0.01f;
            condTime += 0.01f;//Time.deltaTime;
            if (agent.remainingDistance < remDist)
            {
                angle += anglePlus;
                destination = Vec3Mathf.GetCirclePoint(startPos, angle, rad);
                agent.SetDestination(destination);
                //Debug.Log("Добавляю угол:" + anglePlus);
            }
            if (condTime >= 5.0f)
            {
                ChangeCondition(Condition.Safety, "FlyAroundPointSafety", "Safety");
            }
            if (fow.visibleTargets.Count > 0)
            {
                isEnemySpotted = true;
                visibleTarget = fow.visibleTargets[0];
                ChangeCondition(Condition.Alarm, "FlyAroundPointSafety", "Alarm");
            }
            //if(safetyWalkTime > 15.0f)
            //{
            //    ChangeCondition(Condition.Secure, "FlyAroundPointSafety", "GoBackToTheSpot");
            //}
            Debug.DrawLine(startPos, agent.destination, Color.red);
            yield return new WaitForSeconds(0.01f);
        }
    }
    //protected IEnumerator GoBackToTheSpot()
    //{
    //    float speed = 0.1f;
    //    agent.enabled = false;
    //    destination = eagleWaitPoint.position;
    //    traectory.CalcGraph(transform.position, destination);
    //    Vector3 nextP = traectory.GetNextPoint();
    //    transform.rotation = Vec3Mathf.GetDir(transform.position, destination);
    //    while (Vector3.Distance(transform.position, destination) > 1.0f)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, nextP, 0.5f);
    //        //speed += Time.deltaTime;
    //        //transform.position = Vector3.MoveTowards(transform.position, destination, speed);
    //        //Debug.Log(Vector3.Distance(transform.position, destination));
    //        if (Vector3.Distance(transform.position, nextP) < 0.1f)
    //            nextP = traectory.GetNextPoint();
    //        yield return new WaitForEndOfFrame();
    //    }
    //    yield return new WaitForSeconds(1.0f);
    //    Debug.Log("Вернулись в укрытие");
    //    ChangeCondition(Condition.Secure, "GoBackToTheSpot", "Secure");
    //}

    protected override void ChangeCondition(Condition targetCond, string currentCoroutine, string targetCoroutine)
    {
        this.cond = targetCond;
        currentCond = targetCoroutine;
        StartCoroutine(targetCoroutine);
        StopCoroutine(currentCoroutine);
    }
}
