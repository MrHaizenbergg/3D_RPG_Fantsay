
using UnityEngine;

public class EnemyAttackState : State
{
    public EnemyAttackState(EnemyControllerRPG enemyControllerRPG, StateMashine enemyStateMashine) : base(enemyControllerRPG, enemyStateMashine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
    public override void PhysicsUpdate()
    {
        enemyController.SearchPlayer();

        //Debug.Log("EnemyAttackStateActive");
        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }
}
