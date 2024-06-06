using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestRPG> quests=new List<QuestRPG>();

    public Equipment[] equipmentRewards;

    private void Start()
    {
        FirstQuest();
    }

    private void FirstQuest()
    {
        QuestRPG questRPG = new QuestRPG();
    
        questRPG.QuestName = "Ultimate Slayer";
        questRPG.Description = "Kill 3 Skeletons of stuff";
        questRPG.ItemReward = equipmentRewards[0];
        questRPG.ExperienceReward = 100;
        questRPG.GoldReward = 50;

        questRPG.Goals.Add(new KillGoal(questRPG, EnemyType.Skeleton, "Kill 3 Skeleton", false, 0, 1));
        questRPG.Goals.Add(new KillGoal(questRPG, EnemyType.Skeleton_Archer, "Kill 2 Skeleton Archer", false, 0, 1));
        questRPG.Goals.Add(new CollectionGoal(questRPG,"book","Find book how to fast",false, 0, 1));

        questRPG.Goals.ForEach(g => g.Init());
        quests.Add(questRPG);
    }

    private void SecondQuest()
    {
        QuestRPG questRPG = new QuestRPG();

        questRPG.QuestName = "Skeleton Destroyer";
        questRPG.Description = "Kill 2 Skeletons of nahuy";
        questRPG.ItemReward = equipmentRewards[1];
        questRPG.ExperienceReward = 200;
        questRPG.GoldReward = 60;

        questRPG.Goals.Add(new KillGoal(questRPG, EnemyType.Skeleton, "Kill 3 Skeleton", false, 0, 1));
        questRPG.Goals.Add(new KillGoal(questRPG, EnemyType.Skeleton_Archer, "Kill 2 Skeleton Archer", false, 0, 1));

        questRPG.Goals.ForEach(g => g.Init());
        quests.Add(questRPG);
    }
}
