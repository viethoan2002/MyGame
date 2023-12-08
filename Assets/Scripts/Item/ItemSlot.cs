using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSlot 
{
    public int ID;
    public ItemObject item;
    public int amount;

    public ItemSlot(int _id, ItemObject _item,int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void ResetAmount()
    {
        amount = 0;
    }

    public void AddAmount(int _value)
    {
        amount += _value;
    }

    public void RemoveAmount(int _value)
    {
        amount -= _value;
        amount = amount < 0 ? 0 : amount;
    }
}
