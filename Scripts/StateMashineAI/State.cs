using System.Collections;

public class State
{
    protected StateMashine stateMashine;
    protected EnemyControllerRPG enemyController;
    protected HumanAllyController humanAllyController;
   
    public State(EnemyControllerRPG enemyControllerRPG, StateMashine enemyStateMashine) 
    {
        enemyController = enemyControllerRPG;
        stateMashine = enemyStateMashine;
    }

    public State(HumanAllyController humanControllerRPG, StateMashine humanStateMashine)
    {
        humanAllyController = humanControllerRPG;
        stateMashine = humanStateMashine;
    }

    public virtual void EnterState()
    {
        
    }
    public virtual void ExitState()
    {
        
    }
    public virtual void FrameUpdate()     
    {
        
    }
    public virtual void PhysicsUpdate()
    {
        
    }
    public virtual void AnimationTriggerEvent()
    {

    }
}
