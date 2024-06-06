using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KIllGoalSO :Quest.QuestGoal
{
    public string Killing;

    public override string GetDescrtiption()
    {
        return $"Killing a {Killing}";
    }
}
