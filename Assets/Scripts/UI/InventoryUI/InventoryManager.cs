using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public InventoryObject playerInventory;
    public DisplayInventory playerInventoryDisplay;

    public InventoryObject enviromentInventory;
    public DisplayInventory enviromentInventoryDisplay;

    private void Awake()
    {
        InventoryManager.Instance = this;
    }

    public void SetPlayerInventory(InventoryObject _inventory)
    {
        playerInventory = _inventory;
        playerInventoryDisplay.SetInventory(_inventory);
    }

    public void SetEnviromentInventory(InventoryObject _inventory)
    {
        enviromentInventory = _inventory;
        enviromentInventoryDisplay.SetInventory(_inventory);
    }
}
