using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextContruction : MonoBehaviour
{
    public Vector3 position;
    public float angleY;

    public CheckPosition checkPosition;
    public String sceneToLoad;

    private void Update()
    {
        HandleUseNextContruction();
    }

    private void HandleUseNextContruction()
    {
        if (checkPosition.isPlayerHere && InputHandle.Instance.use)
        {
            Vector3 newposition = new Vector3(checkPosition.Player.transform.position.x * position.x,
                                             checkPosition.Player.transform.position.y * position.y,
                                             checkPosition.Player.transform.position.z * position.z);


            SaveLoadManager.Instance.SaveAllInventorys();
            checkPosition.Player.GetComponent<PlayerInventory>().SavePlayer(newposition, angleY);
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
}
