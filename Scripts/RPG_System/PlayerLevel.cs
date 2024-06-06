using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int Level { get; set; }
    public int CurrentExperience { get; set; }
    public int RequiredExperience { get { return Level * 25; } }

    private void Start()
    {
        CombatEvents.OnEnemyDeath += EnemyToExperience;
        Level = 1;
    }

    public void EnemyToExperience(EnemyStats stats)
    {
        GrantExperience(stats.experienceForEnemy);
    }

    public void GrantExperience(int amount)
    {
        CurrentExperience += amount;
        while(CurrentExperience>=RequiredExperience)
        {
            CurrentExperience -= RequiredExperience;
            Level++;
            Debug.Log("CurrentLevel: " + Level);
        }

        PlayerHelathUI.PlayerLeveleChanged();
    }
}
