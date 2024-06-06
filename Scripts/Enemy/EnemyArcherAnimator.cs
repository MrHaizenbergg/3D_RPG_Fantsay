using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherAnimator : CharacterAnimator
{
    EnemyArcherCombat combatArcher;

    protected override void Start()
    {
        combatArcher = GetComponent<EnemyArcherCombat>();
        //combatArcher.OnAttackArcher += OnAttack;
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
