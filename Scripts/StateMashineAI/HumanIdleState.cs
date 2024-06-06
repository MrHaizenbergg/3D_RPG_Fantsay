using UnityEngine;

public class HumanIdleState : State
{
    private int randomWaypoints;

    public HumanIdleState(HumanAllyController humanControllerRPG, StateMashine humanStateMashine) : base(humanControllerRPG, humanStateMashine)
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
        //enemyController.PatrolWayPoints();
        if (randomWaypoints == 1)
            humanAllyController.MoveToCapturedPoint(humanAllyController.wayPointsA);
        if (randomWaypoints == 2)
            humanAllyController.MoveToCapturedPoint(humanAllyController.wayPointsB);
        if (randomWaypoints == 3)
            humanAllyController.MoveToCapturedPoint(humanAllyController.wayPointsC);

        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }
}
