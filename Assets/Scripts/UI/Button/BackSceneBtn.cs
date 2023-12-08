using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackSceneBtn : BaseBtn
{
    public GameObject Player;
    public Vector3 position;
    public float angleY;

    public String sceneToLoad;
    public override void LoadComponent()
    {
    }

    public override void OnClick()
    {
        Player.GetComponent<PlayerInventory>().SavePlayer(position, angleY);
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

}
