using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class EnemyRPG : Interactable
{
    [SerializeField] public HumansMapArea target;
    [SerializeField] private LayerMask enemiesLayer;

    readonly Collider[] collidersNonAlloc = new Collider[50];

    private int size;
    private float tickCooldownFindHumans = 1;

    private HumansMapArea enemy;

    public event EventHandler EnemyFindTargetEvent;

    private void Start()
    {
        //StartCoroutine(SearchForEnemies());
    }

    protected override void Update()
    {
        tickCooldownFindHumans -= Time.deltaTime;

        if (tickCooldownFindHumans <= 0)
        {
            if (target != null)
                EnemyFindTargetEvent?.Invoke(this, EventArgs.Empty);

            SearchEnemies();

            tickCooldownFindHumans = 1;
        }
    }

    private void SearchEnemies()
    {
        size = Physics.OverlapSphereNonAlloc(interactionTransform.position, radius, collidersNonAlloc, enemiesLayer);

        for (int i = 0; i < size; i++)
        {
            enemy = collidersNonAlloc[i].GetComponent<HumansMapArea>();

            if (enemy != null)
            {
                target = enemy;
            }
        }
    }

    //IEnumerator SearchForEnemies()
    //{
    //    var size = Physics.OverlapSphereNonAlloc(transform.position, radius, collidersNonAlloc, enemiesLayer);

    //    for (int i = 0; i < size; i++)
    //    {
    //        var enemy = collidersNonAlloc[i].GetComponent<HumansMapArea>();

    //        if (enemy != null)
    //        {
    //            target = enemy.transform;
    //        }
    //    }

    //    yield return new WaitForSeconds(1);
    //}

    public HumansMapArea GetTarget()
    {
        if (target != null)
            return target;
        else
            return null;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
