using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Equipment,
    Weapon,
    Default
}

public class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public GameObject prefabUI;
    public float weight;
    public ItemType type;
    public Sprite icon;
    public string nameItem;
    [TextArea(15, 20)]
    public string description;
}

