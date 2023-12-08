using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonQuest : QuestStep
{
    public List<QuestStep> questSteps = new List<QuestStep>();
    public GameObject Chest;

    private void OnEnable()
    {
        DoorStep.FinishQuest += CheckQuest;
    }

    private void OnDisable()
    {
        DoorStep.FinishQuest -= CheckQuest;
    }

    public void CheckQuest(QuestStep quest)
    {
        if (questSteps.Contains(quest))
        {
            questSteps.Remove(quest);
            if (questSteps.Count == 0)
            {
                FinishQuestStep();
            }
        }
    }

    public override void FinishQuestStep()
    {
        Chest.SetActive(true);
    }
}
