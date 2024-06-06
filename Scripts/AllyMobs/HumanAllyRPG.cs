using System;
using UnityEngine;

public class HumanAllyRPG : Interactable
{
    [SerializeField] public EnemyStats target;
    [SerializeField] private LayerMask enemiesLayer;

    private float tickCooldownFindEnemyes = 1;

    private int size;

    private EnemyStats enemy;

    readonly Collider[] collidersNonAlloc = new Collider[50];

    public event EventHandler FindTargetEvent;

    private void Start()
    {
        //StartCoroutine(SearchForEnemies());
    }

    protected override void Update()
    {
        tickCooldownFindEnemyes -= Time.deltaTime;

        if (tickCooldownFindEnemyes <= 0)
        {
            if (target != null)
                FindTargetEvent?.Invoke(this, EventArgs.Empty);

            SearchEnemies();

            tickCooldownFindEnemyes = 1;
            //Debug.Log("TickFindEnemies");
        }
    }

    private void SearchEnemies()
    {
        size = Physics.OverlapSphereNonAlloc(interactionTransform.position, radius, collidersNonAlloc, enemiesLayer);

        for (int i = 0; i < size; i++)
        {
            enemy = collidersNonAlloc[i].GetComponent<EnemyStats>();

            if (enemy != null)
            {
                target = enemy;
            }
        }
    }

    public EnemyStats GetTarget()
    {
        if (target != null)
        {
            return target;
        }
        else
        {
            return null;
        }
    }

    //IEnumerator SearchForEnemies()
    //{
    //    var size = Physics.OverlapSphereNonAlloc(transform.position, radius, collidersNonAlloc, enemiesLayer);

    //    for (int i = 0; i < size; i++)
    //    {
    //        var enemy = collidersNonAlloc[i].GetComponent<EnemyStats>();

    //        if (enemy != null)
    //        {
    //            target = enemy.transform;
    //        }
    //    }

    //    yield return new WaitForSeconds(1);
    //}

    //Transform GetClosestEnemy(List<Transform> enemies)
    //{
    //    Transform bestTarget = null;
    //    float closestDistanceSqr = Mathf.Infinity;
    //    Vector3 currentPosition = transform.position;
    //    foreach (Transform potentialTarget in enemies)
    //    {
    //        Vector3 directionToTarget = potentialTarget.position - currentPosition;
    //        float dSqrToTarget = directionToTarget.sqrMagnitude;
    //        if (dSqrToTarget < closestDistanceSqr)
    //        {
    //            closestDistanceSqr = dSqrToTarget;
    //            bestTarget = potentialTarget;
    //        }
    //    }

    //    return bestTarget;
    //}


    //public EnemyStats GetTarget()
    //{
    //    if (target != null)
    //        return target;
    //    else
    //        return null;
    //}

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
