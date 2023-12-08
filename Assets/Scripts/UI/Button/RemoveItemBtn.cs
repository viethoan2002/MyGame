using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemBtn : BaseBtn
{
    public DisplayInventory displayInventory;
    public ItemObject item;
    public int value;

    public override void LoadComponent()
    {
       
    }

    public override void OnClick()
    {
        displayInventory.RemoveItem(item, value);
    }
}
