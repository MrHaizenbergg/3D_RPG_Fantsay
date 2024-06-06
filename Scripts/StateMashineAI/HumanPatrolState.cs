using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPatrolState : State
{
    private int randomWaypoints;

    public HumanPatrolState(HumanAllyController humanControllerRPG, StateMashine humanStateMashine) : base(humanControllerRPG, humanStateMashine)
    {
    }

    public override void EnterState()
    {
        randomWaypoints = UnityEngine.Random.Range(1, 3);
        humanAllyController.currentHumanWaypoints = randomWaypoints;
        Debug.Log("RandomWaypointPatrol: " + randomWaypoints);
        humanAllyController.SetStateMove();

        base.EnterState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        //enemyController.PatrolWayPoints();
        base.FrameUpdate();
    }
    public override void PhysicsUpdate()
    {
        if (humanAllyController.currentHumanWaypoints == 1)
            humanAllyController.MoveToCapturedPoint(humanAllyController.wayPointsA);
        else if (humanAllyController.currentHumanWaypoints == 2)
            humanAllyController.MoveToCapturedPoint(humanAllyController.wayPointsB);
        else if (humanAllyController.currentHumanWaypoints == 3)
            humanAllyController.MoveToCapturedPoint(humanAllyController.wayPointsC);

        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }
}
