using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStep : QuestStep
{
    public GameObject door;

    public int pointNeedToComplete;
    public int currentPoint;

    public static event Action<QuestStep> FinishQuest;

    private void OnEnable()
    {
        DeadState.AddPoint += AddPoint;
    }

    private void OnDisable()
    {
        DeadState.AddPoint -= AddPoint;
    }

    public void AddPoint()
    {
        currentPoint += 1;
        if (currentPoint >= pointNeedToComplete)
        {
            FinishQuestStep();
        }
    }

    public override void FinishQuestStep()
    {
        if (isStart)
        {
            door.transform.Rotate(door.transform.up, 90);
            FinishQuest?.Invoke(this);
        }
    }
}
