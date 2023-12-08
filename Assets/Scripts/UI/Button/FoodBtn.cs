using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FoodBtn : UseBtn
{
    public override void EquipToPlayer()
    {
        displayInventory.RemoveItem(itemSlot.item, 1);
        FoodObject food = (FoodObject)itemSlot.item;
        playerInventory.Health(food.restoreGealthValue);
    }

    public override bool PlayAnimation()
    {
        return true;
    }
}
