using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartQuest : MonoBehaviour
{
    public bool isStart;
    public DoorStep questStep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isStart)
        {
            questStep.QuestStart();
            isStart = true;
        }
    }
}
