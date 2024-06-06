using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiverRPG : NPC
{
    public bool assignedQuest { get; set; }
    public bool helped { get; set; }

    public GameObject questWindow;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text experienceText;
    public TMP_Text goldText;

    public Button acceptButton;

    public QuestRPG questRpg;
    public QuestManager questManager;

    public override void Interact()
    {
        if (!assignedQuest && !helped)
        {
            base.Interact();
            OpenQuestWindow();
        }
        else if (assignedQuest && !helped)
        {
            CheckProgressQuest();
        }
        else
        {
            //Разговор когда нет квестов
        }
    }

    public void OpenQuestWindow()
    {
        int randomIndex = Random.Range(0, questManager.quests.Count);
        questRpg = questManager.quests[randomIndex];

        questWindow.SetActive(true);
        titleText.text = questRpg.QuestName;
        Debug.Log("QuestName: " + questRpg.QuestName);
        descriptionText.text = questRpg.Description;
        experienceText.text = questRpg.ExperienceReward.ToString();
        goldText.text = questRpg.GoldReward.ToString();

        acceptButton.onClick.AddListener(AssignQuest);
    }

    private void AssignQuest()
    {
        questWindow.SetActive(false);
        assignedQuest = true;
    }
    private void CheckProgressQuest()
    {
        if (questRpg.Completed)
        {
            acceptButton.onClick.RemoveListener(AssignQuest);

            questRpg.GiveReward();
            helped = true;
            assignedQuest = false;
            Debug.Log("QuestCompleted");
            //Диалог после выполнения квеста
        }
        else
        {
            //Вы не вполнили задание
        }
    }
}
