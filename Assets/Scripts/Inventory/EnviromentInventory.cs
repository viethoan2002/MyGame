using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public CheckPosition checkPosition;

    public bool isShowUI;

    private void Awake()
    {
        inventory.Load();
    }

    private void Update()
    {
        if(InputHandle.Instance.use && checkPosition.isPlayerHere && !isShowUI)
        {
            InventoryManager.Instance.SetEnviromentInventory(inventory);
            Vector3 target = transform.position;
            target.y = checkPosition.Player.transform.position.y;
            checkPosition.Player.transform.LookAt(transform);
            isShowUI = true;
        }

        if (!checkPosition.isPlayerHere && isShowUI)
        {
            InventoryManager.Instance.SetEnviromentInventory(null);
            isShowUI = false;
        }
    }

}
