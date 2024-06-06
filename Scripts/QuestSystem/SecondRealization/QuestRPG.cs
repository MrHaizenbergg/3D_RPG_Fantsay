using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestRPG
{
    public List<Goal> Goals = new List<Goal>();
    public string QuestName{  get; set; }
    public string Description{ get; set; }
    public int ExperienceReward{  get; set; }
    public int GoldReward{  get; set; }
    public ItemRPG ItemReward;
    public bool Completed;

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
    }

    public void GiveReward()
    {
        if(ItemReward != null)
        {
            InventoryRPG.instance.Add(ItemReward);
            Debug.Log("Reward: " + ItemReward.name);
        }
    }
}
