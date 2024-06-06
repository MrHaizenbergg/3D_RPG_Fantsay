
using UnityEngine;

public class KillGoal : Goal
{
    public EnemyType enemyType {  get; set; }

    public KillGoal(QuestRPG quest,EnemyType enemyType,string description,bool completed,int currentAmount,int requiredAmout)
    {
        this.QuestRpg = quest;
        this.enemyType = enemyType;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmoint = requiredAmout;
    }

    public override void Init()
    {
        base.Init();
        CombatEvents.OnEnemyDeath += EnemyDieQuest;
    }

    private void EnemyDieQuest(EnemyStats enemy)
    {
        if(enemy.enemyType==this.enemyType)
        {
            this.CurrentAmount ++;
            Debug.Log("EnType: " + enemy.enemyType + " Killed");
            Evaluate();
        }
    }
}
