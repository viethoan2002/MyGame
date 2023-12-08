using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory List", menuName = "Inventory System/InventoryList")]
public class InventoryList : ScriptableObject
{
    public List<InventoryObject> inventoryList;
}
