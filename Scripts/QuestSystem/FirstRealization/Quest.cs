using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Quest : ScriptableObject
{
    [System.Serializable]
    public struct Info
    {
        public string Name;
        public Sprite Icon;
        public string Description;
    }

    [Header("Info")] public Info information;

    [System.Serializable]
    public struct Stat
    {
        public int Coin;
        public int Experience;
    }

    [Header("Reward")] public Stat Reward = new Stat { Coin = 10, Experience = 10 };

    public bool Completed { get; protected set; }
    public QuestCompletedEvent QuestCompleted;

    public abstract class QuestGoal : ScriptableObject
    {
        protected string Description;
        public int CurrentAmount { get; protected set; }
        public int RequiredAmount = 1;

        public bool Completed { get; protected set; }
        [HideInInspector] public UnityEvent GoalCompleted;

        public virtual string GetDescrtiption()
        {
            return Description;
        }

        public virtual void Initialize()
        {
            Completed = false;
            GoalCompleted=new UnityEvent();
        }

        protected void Evaluate()
        {
            if (CurrentAmount >= RequiredAmount)
            {
                Complete();
            }
        }

        private void Complete()
        {
            Completed=true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }

        public void Skip()
        {
            //Ќачислите игроку некоторую игровую валюту
        }
    }

    public List<QuestGoal> Goals;

    public void Initialize()
    {
        Completed = false;
        QuestCompleted=new QuestCompletedEvent();

        foreach (var goal in Goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    private void CheckGoals()
    {
        Completed=Goals.All(x => x.Completed);
        if(Completed)
        {
            //give reward
            QuestCompleted.Invoke(this);
            QuestCompleted.RemoveAllListeners();
        }
    }
}

public class QuestCompletedEvent : UnityEvent<Quest> { }
