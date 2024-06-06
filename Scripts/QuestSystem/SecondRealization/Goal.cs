
public class Goal
{
    public QuestRPG QuestRpg { get; set; }

    public string Description {  get; set; }
    public bool Completed {  get; set; }
    public int CurrentAmount;
    public int RequiredAmoint;

    public virtual void Init()
    {
        //default init stuff
    }

    public void Evaluate()
    {
        if(CurrentAmount >= RequiredAmoint)
        {
            Complete();
        }
    }

    public void Complete()
    {
        Completed=true;
        QuestRpg.CheckGoals();
    }
}
