
using UnityEngine.Events;

public class EnemyStats : CharacterStats
{
    public EnemyType enemyType;

    public int experienceForEnemy;

    public UnityEvent enemyDieEvent;

    public bool isDead=false;

    EnemyStats enemy;

    public void DestroyEnemyForEvent()
    {
        Invoke("DestroyEnemy", 2);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public override void Die()
    {
        base.Die();
        isDead = true;
        CombatEvents.EnemyDied(this);
        enemyDieEvent.Invoke();  
    }
}

public enum EnemyType
{
    Skeleton,
    Skeleton_Archer,
    Skeleton_Mage
}
