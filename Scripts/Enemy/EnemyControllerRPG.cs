using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct Point
{
    public int numberOfPoint;
    public MapAreaCollider.State state;

    public Point(int numberOfPoint, MapAreaCollider.State state)
    {
        this.numberOfPoint = numberOfPoint;
        this.state = state;
    }
}

public class EnemyControllerRPG : MonoBehaviour
{
    [SerializeField] protected float lookRadius = 10f;
    [SerializeField] protected float patrolTime;

    protected int wpID;
    public List<Transform> wayPoints;

    private Transform enemyTransform;

    protected EnemyRPG enemyRPG;

    protected HumansMapArea targetHuman;
    private Transform player;
    protected NavMeshAgent agent;
    protected CharacterCombat combat;

    [Tooltip("Radius at which the enemy will notice the player if the player is too close (if player crouch this value = / 2) ")]
    public float closeDistance;
    [Tooltip("The time after which the enemy loses the player(after the time has passed, the player’s coordinates are transferred to the enemy and he goes to these coordinates)")]
    public float lostTime;
    [Tooltip("Transform of enemy head")]
    public Transform head;
    [Tooltip("Enemy Field of View (x - minimal, y - maximal)")]
    public Vector2 enemyFOV;
    public float pathUpdateDelay = 0.2f;

    [SerializeField] protected bool seePlayer;
    protected bool chasePlayer;
    protected bool searchPlayer;
    protected int searchState;

    public MapAreaCollider[] tempPoints;

    public List<Transform> wayPointsA;
    public List<Transform> wayPointsB;
    public List<Transform> wayPointsC;

    public Point[] points;

    public StateMashine stateMashine { get; set; }
    public EnemyIdleState idleState { get; set; }
    public EnemyAttackState attackState { get; set; }
    public EnemyPatrolState patrolState { get; set; }

    private HumansMapArea playerComponent;
    protected float distance;

    public int currentWaypoints;
    protected int capturePointRandom;

    protected Vector3 lastSawPoint;
    protected Vector3 lookPose;
    protected Quaternion rotation;

    protected virtual void Awake()
    {
        new List<MapAreaCollider>();

        enemyTransform = transform;

        enemyRPG = GetComponent<EnemyRPG>();

        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();

        tempPoints = FindObjectsOfType(typeof(MapAreaCollider)) as MapAreaCollider[];
        points = new Point[tempPoints.Length];

        stateMashine = new StateMashine();
        idleState = new EnemyIdleState(this, stateMashine);
        attackState = new EnemyAttackState(this, stateMashine);
        patrolState = new EnemyPatrolState(this, stateMashine);
    }

    protected virtual void Start()
    {
        enemyRPG.EnemyFindTargetEvent += ActiveAttackStateEnemy;

        wayPointsA = GameControll.instance.wayPointsA_GameCon;
        wayPointsB = GameControll.instance.wayPointsB_GameCon;
        wayPointsC = GameControll.instance.wayPointsC_GameCon;

        lastSawPoint = Vector3.zero;
        wpID = -1;
        player = PlayerManager.instance.player.transform;
        playerComponent = player.GetComponent<HumansMapArea>();
        stateMashine.Initialize(idleState);

        foreach (MapAreaCollider point in tempPoints)
        {
            point.OnCaptured += CheckCurrentStatePoints;
            point.OnCapturedMonsters += CheckCurrentStatePoints;
        }
    }

    protected virtual void OnDisable()
    {
        enemyRPG.EnemyFindTargetEvent -= ActiveAttackStateEnemy;

        foreach (MapAreaCollider point in tempPoints)
        {
            point.OnCaptured -= CheckCurrentStatePoints;
            point.OnCapturedMonsters -= CheckCurrentStatePoints;
        }
    }

    public virtual void FixedUpdate()
    {
        stateMashine.state.PhysicsUpdate();
    }

    public virtual void Update()
    {
        targetHuman = enemyRPG.GetTarget();
    }

    private void ActiveAttackStateEnemy(object sender, EventArgs e)
    {
        stateMashine.ChangeState(attackState);
        Debug.Log("EnemyAttackStateActive");
    }

    protected virtual void CheckCurrentStatePoints(object sender, EventArgs e)
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
                stateMashine.ChangeState(patrolState);
            }
            if (point.state == MapAreaCollider.State.CapturedHumans)
            {
                stateMashine.ChangeState(patrolState);
            }
        }
    }

    public virtual void SetStateMove()
    {
        foreach (Point point in points)
        {
            if (point.state == MapAreaCollider.State.Neutral || point.state == MapAreaCollider.State.CapturedHumans)
            {
                if (point.numberOfPoint == 1)
                {
                    currentWaypoints = 1;
                    Debug.Log("MovePointActive: " + currentWaypoints);
                }
                if (point.numberOfPoint == 2)
                {
                    currentWaypoints = 2;
                    Debug.Log("MovePointActive: " + currentWaypoints);
                }
                if (point.numberOfPoint == 3)
                {
                    currentWaypoints = 3;
                    Debug.Log("MovePointActive: " + currentWaypoints);
                }
            }
        }
    }

    public virtual void MoveToCapturedPoint(List<Transform> waypoints)
    {
        capturePointRandom = UnityEngine.Random.Range(1, waypoints.Count);   //massive Length
        EnemySetDestination(waypoints[capturePointRandom].position);
    }

    public virtual void AttackPlayer()
    {
        if (targetHuman != null)
        {
            distance = Vector3.Distance(targetHuman.gameObject.transform.position, enemyTransform.position);

            if (distance <= lookRadius)
            {
                LookAtTarget(targetHuman.gameObject.transform);
                UpdatePath(targetHuman.gameObject.transform);

                if (distance <= agent.stoppingDistance + 0.3f)
                {
                    combat.AttackEnemy(targetHuman.gameObject.GetComponent<CharacterStats>());
                }
            }
        }

    }

    protected void LookAtTarget(Transform targetPos)
    {
        lookPose = targetPos.position - transform.position;
        lookPose.y = 0;
        rotation = Quaternion.LookRotation(lookPose);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    protected void UpdatePath(Transform target)
    {
        if (Time.time >= pathUpdateDelay)
        {
            Debug.Log("UpdatingPath");
            pathUpdateDelay = Time.time + pathUpdateDelay;
            agent.SetDestination(target.position);
        }
    }

    //private void UpdatePath(Vector3 target)
    //{
    //    if (Time.time >= pathUpdateDelay)
    //    {
    //        Debug.Log("UpdatingPath");
    //        pathUpdateDelay = Time.time + pathUpdateDelay;
    //        agent.SetDestination(target);
    //    }
    //}

    public virtual void SearchPlayer()
    {
        if (targetHuman == null)
        {
            stateMashine.ChangeState(idleState);
        }

        AttackPlayer();

        chasePlayer = true;
        seePlayer = true;

        ResetSearchStates();

        if (seePlayer)
        {
            if (!searchPlayer)
            {
                seePlayer = false;
                searchPlayer = true;
                StartCoroutine(WaitLostTime());
            }
        }
        else
        {
            seePlayer = false;

        }

        if (seePlayer || chasePlayer)
        {
            if (targetHuman != null)
                EnemySetDestination(targetHuman.transform.position);
        }
    }

    private void CheckLastPoint()
    {
        if (!seePlayer && lastSawPoint != Vector3.zero && searchState == 0)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(lastSawPoint, out hit, 1.5f, NavMesh.AllAreas))
            {
                lastSawPoint = hit.position;
            }

            EnemySetDestination(lastSawPoint);

            float dist = Vector3.Distance(transform.position, lastSawPoint);

            if (dist <= 2f)
            {
                EnemySetDestination(transform.position);
                lastSawPoint = Vector3.zero;
                searchState = 1;
                //randomMonolog = Random.Range(0, monologs.Length);
                //monologS.volume = AS.volume;

                //if (currentLevel == 1)
                //{
                //    if (Language.Instance.currentLanguage == "en")
                //        monologS.PlayOneShot(enMonologs[randomMonolog]);
                //    else if (Language.Instance.currentLanguage == "ru")
                //        monologS.PlayOneShot(monologs[randomMonolog]);
                //    else
                //        monologS.PlayOneShot(enMonologs[randomMonolog]);
                //}
                //else
                //{
                //    monologS.PlayOneShot(monologs[randomMonolog]);
                //}

                StartCoroutine(WaitPatrolTime(1));
            }
        }
    }

    protected bool PlayerFOV()
    {
        Vector3 targetDir = targetHuman.transform.position - head.transform.position;
        float angleToPlayer = (Vector3.Angle(targetDir, head.forward));

        if (angleToPlayer >= enemyFOV.x && angleToPlayer <= enemyFOV.y) // 180° FOV
        {

            return true;
        }

        return false;
    }

    //protected bool PlayerRaycast()
    //{
    //    RaycastHit hit;

    //    if (targetHuman != null)
    //    {

    //        if (Physics.Raycast(transform.position, (targetHuman.transform.position - transform.position), out hit, lookRadius))
    //        {
    //            if (hit.transform.gameObject == targetHuman.gameObject)
    //            {
    //                return true;
    //                //Debug.Log("PlayerRaycast");
    //            }
    //        }

    //    }
    //    return false;
    //}

    protected IEnumerator WaitLostTime()
    {
        yield return new WaitForSeconds(lostTime);

        chasePlayer = false;
    }

    public virtual void PatrolWayPoints()
    {
        if (!seePlayer && !searchPlayer && wpID == -1)
        {
            StopAllCoroutines();
            int r = UnityEngine.Random.Range(1, wayPoints.Count);   //massive Length
            EnemySetDestination(wayPoints[r].position);
            wpID = r;
            wayPoints[r].SetSiblingIndex(0);

        }

        if (wpID != -1)
        {
            float dist = Vector3.Distance(transform.position, wayPoints[wpID].position);
            if (dist <= agent.stoppingDistance)
            {
                EnemySetDestination(transform.position);
                StartCoroutine(WaitPatrolTime(0));
            }
        }
    }

    protected IEnumerator WaitPatrolTime(int state)
    {
        yield return new WaitForSeconds(patrolTime);
        if (state == 1)
        {
            //lastSawPoint = Vector3.zero;
        }
        ResetSearchStates();
    }
    protected void ResetSearchStates()
    {
        wpID = -1;
    }

    protected void EnemySetDestination(Vector3 pos)
    {
        if (agent.enabled)
        {
            //UpdatePath(pos);
            agent.SetDestination(pos);
        }
    }

    public void SendHidePlace()
    {
        lastSawPoint = Vector3.zero;
        //playerHidePlace = player.hidePlace;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
