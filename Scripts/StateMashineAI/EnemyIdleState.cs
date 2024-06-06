using UnityEngine;

public class EnemyIdleState : State
{
    private int randomWaypoints;

    public EnemyIdleState(EnemyControllerRPG enemyControllerRPG, StateMashine enemyStateMashine) : base(enemyControllerRPG, enemyStateMashine)
    {
    }

    public override void EnterState()
    {
        randomWaypoints = UnityEngine.Random.Range(1, 3);
        Debug.Log("RandomWaypoint: " + randomWaypoints);

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
        if (randomWaypoints == 1)
            enemyController.MoveToCapturedPoint(enemyController.wayPointsA);
        if (randomWaypoints == 2)
            enemyController.MoveToCapturedPoint(enemyController.wayPointsB);
        if (randomWaypoints == 3)
            enemyController.MoveToCapturedPoint(enemyController.wayPointsC);

        //Debug.Log("EnemyIdleStateActive");

        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }
}
