
using UnityEngine;

public class EnemyPatrolState : State
{
    public EnemyPatrolState(EnemyControllerRPG enemyControllerRPG, StateMashine enemyStateMashine) : base(enemyControllerRPG, enemyStateMashine)
    {
    }

    public override void EnterState()
    {
        enemyController.SetStateMove();

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
        if (enemyController.currentWaypoints == 1)
            enemyController.MoveToCapturedPoint(enemyController.wayPointsA);
        else if(enemyController.currentWaypoints == 2)
            enemyController.MoveToCapturedPoint(enemyController.wayPointsB);
        else if(enemyController.currentWaypoints==3)
            enemyController.MoveToCapturedPoint(enemyController.wayPointsC);

        //Debug.Log("EnemyPatrolStateActive");

        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }
}
