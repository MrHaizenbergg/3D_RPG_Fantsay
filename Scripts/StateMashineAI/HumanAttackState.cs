using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttackState : State
{
    public HumanAttackState(HumanAllyController humanControllerRPG, StateMashine humanStateMashine) : base(humanControllerRPG, humanStateMashine)
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
        humanAllyController.SearchPlayer();
        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }
}
