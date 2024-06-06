using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanAllyController : EnemyControllerRPG
{
    private HumanAllyRPG allyRPG;
    private Transform humanTransform;

    private bool seeEnemy;
    private EnemyStats targetEnemy;

    public int currentHumanWaypoints;

    public StateMashine stateMashineHuman { get; set; }
    public HumanIdleState idleStateHuman { get; set; }
    public HumanAttackState attackStateHuman { get; set; }
    public HumanPatrolState patrolStateHuman { get; set; }

    protected override void Awake()
    {
        new List<MapAreaCollider>();
        allyRPG = GetComponent<HumanAllyRPG>();

        humanTransform = transform;

        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();

        tempPoints = FindObjectsOfType(typeof(MapAreaCollider)) as MapAreaCollider[];
        points = new Point[tempPoints.Length];

        stateMashineHuman = new StateMashine();
        idleStateHuman = new HumanIdleState(this, stateMashine);
        attackStateHuman = new HumanAttackState(this, stateMashine);
        patrolStateHuman = new HumanPatrolState(this, stateMashine);
    }

    protected override void Start()
    {
        allyRPG.FindTargetEvent += ActiveAttackState;

        wayPointsA = GameControll.instance.wayPointsA_GameCon;
        wayPointsB = GameControll.instance.wayPointsB_GameCon;
        wayPointsC = GameControll.instance.wayPointsC_GameCon;

        lastSawPoint = Vector3.zero;
        wpID = -1;
        //target = PlayerManager.instance.player.transform;
        stateMashineHuman.Initialize(idleStateHuman);

        foreach (MapAreaCollider point in tempPoints)
        {
            point.OnCaptured += CheckCurrentStatePoints;
            point.OnCapturedMonsters += CheckCurrentStatePoints;
        }
    }

    protected override void OnDisable()
    {
        allyRPG.FindTargetEvent -= ActiveAttackState;

        foreach (MapAreaCollider point in tempPoints)
        {
            point.OnCaptured -= CheckCurrentStatePoints;
            point.OnCapturedMonsters -= CheckCurrentStatePoints;
        }
    }

    protected override void CheckCurrentStatePoints(object sender, EventArgs e)
    {
        for (int i = 0; i < tempPoints.Length; i++)
        {
            points[i] = new Point(tempPoints[i].numberOfPoint, tempPoints[i].state);
            Debug.Log(points[i].numberOfPoint + " numberPoint " + points[i].state + " state");
        }

        foreach (Point point in points)
        {
            if (point.state == MapAreaCollider.State.Neutral)
            {
                stateMashineHuman.ChangeState(patrolStateHuman);
            }
            if (point.state == MapAreaCollider.State.CapturedMonsters)
            {
                stateMashineHuman.ChangeState(patrolStateHuman);
            }
        }
    }

    public override void AttackPlayer()
    {
        if (targetEnemy != null)
        {
             distance = Vector3.Distance(targetEnemy.gameObject.transform.position, humanTransform.position);

            if (distance <= lookRadius)
            {
                LookAtTarget(targetEnemy.gameObject.transform);
                UpdatePath(targetEnemy.gameObject.transform);

                if (distance <= agent.stoppingDistance + 0.3f)
                {
                    combat.AttackEnemy(targetEnemy);
                }
            }
        }
    }

    public override void SetStateMove()
    {
        foreach (Point point in points)
        {
            if (point.state == MapAreaCollider.State.Neutral || point.state == MapAreaCollider.State.CapturedMonsters)
            {
                if (point.numberOfPoint == 1)
                {
                    currentHumanWaypoints = 1;
                    Debug.Log("MovePointActive: " + currentHumanWaypoints);
                }
                if (point.numberOfPoint == 2)
                {
                    currentHumanWaypoints = 2;
                    Debug.Log("MovePointActive: " + currentHumanWaypoints);
                }
                if (point.numberOfPoint == 3)
                {
                    currentHumanWaypoints = 3;
                    Debug.Log("MovePointActive: " + currentHumanWaypoints);
                }
            }
        }
    }

    public override void SearchPlayer()
    {
        if (targetEnemy == null)
        {
            stateMashineHuman.ChangeState(idleStateHuman);
        }

        AttackPlayer();

        chasePlayer = true;
        seeEnemy = true;

        ResetSearchStates();
        StopAllCoroutines();

        if (seeEnemy)
        {
            if (!searchPlayer)
            {
                seeEnemy = false;
                searchPlayer = true;
                StartCoroutine(WaitLostTimeHuman());
            }
        }
        else
        {
            seeEnemy = false;
        }

        if (seeEnemy || chasePlayer)
        {
            if (targetEnemy != null)
                EnemySetDestination(targetEnemy.transform.position);
        }
    }

    protected IEnumerator WaitLostTimeHuman()
    {
        yield return new WaitForSeconds(lostTime);

        chasePlayer = false;
    }

    public override void MoveToCapturedPoint(List<Transform> waypoints)
    {
        //wpID = -1;
        //Debug.Log("MoveToCaptureBegin " + seeEnemy + "-SeeEnemy " + searchPlayer + "-SearchPlayer");
        //if (!seeEnemy && !searchPlayer && wpID == -1)
        //
        //Debug.Log("HumanMoveCaptureWaypoints");
        //StopAllCoroutines();
        capturePointRandom = UnityEngine.Random.Range(1, waypoints.Count);   //massive Length
        EnemySetDestination(waypoints[capturePointRandom].position);
        //wpID = r;
        //waypoints[r].SetSiblingIndex(0);
        //}

        //if (wpID != -1)
        //{
        //    Debug.Log("HumanMoveCaptureWaypoints Wp-1");
        //    float dist = Vector3.Distance(transform.position, waypoints[wpID].position);
        //    if (dist <= agent.stoppingDistance)
        //    {
        //        EnemySetDestination(transform.position);
        //        StartCoroutine(WaitPatrolTime(0));
        //    }
        //}
    }

    protected bool HumanFOV()
    {
        Vector3 targetDir = targetEnemy.transform.position - head.transform.position;
        float angleToPlayer = (Vector3.Angle(targetDir, head.forward));

        if (angleToPlayer >= enemyFOV.x && angleToPlayer <= enemyFOV.y) // 180° FOV
        {

            return true;
        }

        return false;
    }

    protected bool EnemyRaycast()
    {
        if (targetEnemy != null)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, (targetEnemy.transform.position - transform.position), out hit, lookRadius))
            {
                if (hit.transform.gameObject == targetEnemy.gameObject)
                {
                    return true;
                    //Debug.Log("PlayerRaycast");
                }
            }
        }
        return false;
    }

    public override void Update()
    {
        targetEnemy = allyRPG.GetTarget();
    }

    private void ActiveAttackState(object sender, EventArgs e)
    {
        stateMashineHuman.ChangeState(attackStateHuman);
        //Debug.Log("HumanAttackStateActive");
    }

    public override void FixedUpdate()
    {
        stateMashineHuman.state.PhysicsUpdate();
    }
}
