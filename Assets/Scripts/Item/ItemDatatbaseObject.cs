using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Database",menuName ="Inventory System/Items/Database")]
public class ItemDatatbaseObject : ScriptableObject,ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    public Dictionary<ItemObject, int> GetID = new Dictionary<ItemObject, int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int,ItemObject>();

    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<ItemObject, int>();
        GetItem = new Dictionary<int, ItemObject>();
        for(int i = 0; i < Items.Length; i++)
        {
            GetID.Add(Items[i], i);
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
