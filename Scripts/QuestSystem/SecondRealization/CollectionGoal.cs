using UnityEngine;

public class CollectionGoal : Goal
{
    public string ItemName {  get; set; }

    public CollectionGoal(QuestRPG quest, string itemName, string description, bool completed, int currentAmount, int requiredAmout)
    {
        this.QuestRpg = quest;
        this.ItemName = itemName;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmoint = requiredAmout;
    }

    public override void Init()
    {
        base.Init();
        InventoryRPG.OnItemCheck += ItemPickedUp;
    }

    private void ItemPickedUp(ItemRPG item)
    {
        if (item.name == this.ItemName)
        {
            this.CurrentAmount++;
            Debug.Log("ItemType: " + item.name + " PickUP");
            Evaluate();
        }
    }
}
