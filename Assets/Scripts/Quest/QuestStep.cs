using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    public bool isStart;
    public static event Action<QuestStep> OnStart;

    public void QuestStart()
    {
        OnStart?.Invoke(this);
        isStart = true;
    }
   
    public abstract void FinishQuestStep();
}
