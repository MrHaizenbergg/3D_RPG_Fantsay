using UnityEngine.Events;

public class HumanStats : CharacterStats
{
    public UnityEvent humanDieEvent;

    public bool isDeadHuman = false;

    public void DestroyHumanForEvent()
    {
        Invoke("DestroyHuman", 2);
    }

    private void DestroyHuman()
    {
        Destroy(gameObject);
    }

    public override void Die()
    {
        isDeadHuman = true;
        base.Die();
        //Add ragdoll effect
        humanDieEvent.Invoke();
    }
}
