using UnityEngine;

public class CombatEvents : MonoBehaviour
{
    public delegate void EnemyEventHandler(EnemyStats enemy);
    public static event EnemyEventHandler OnEnemyDeath;

    public static void EnemyDied(EnemyStats enemy)
    {
        if (OnEnemyDeath != null)
            OnEnemyDeath(enemy);
    }
}
