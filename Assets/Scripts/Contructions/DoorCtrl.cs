using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCtrl : MonoBehaviour
{
    public Vector3 position;
    public float angleY;

    public CheckPosition checkPosition;
    public String sceneToLoad;

    private void Update()
    {
        HandleUseDoor();
    }

    private void HandleUseDoor()
    {
        if (checkPosition.isPlayerHere && InputHandle.Instance.use)
        {
            SaveLoadManager.Instance.SaveAllInventorys();
            checkPosition.Player.GetComponent<PlayerInventory>().SavePlayer(position, angleY);
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
}
