using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBtn : UseBtn
{
    public override void LoadComponent()
    {
        if (playerInventory.playerSkin.equipment == (EquipmentObject)itemSlot.item)
        {
            text.text = "Cởi";

        }
        else
        {
            text.text = "Mặc";
        }
    }

    public override void EquipToPlayer()
    {
        if (playerInventory.playerSkin.equipment != (EquipmentObject)itemSlot.item)
        {
            EquipmentObject equipment = (EquipmentObject)itemSlot.item;
            playerInventory.UseSkin(equipment);
            for (int i = 0; i < displayInventory.inventory.Container.Items.Count; i++)
            {
                if (displayInventory.inventory.Container.Items[i].item.type == ItemType.Equipment)
                {
                    displayInventory.itemsDisplayed[displayInventory.inventory.Container.Items[i]].GetComponentInChildren<EquipmentBtn>().text.text = "Mặc";
                }
            }
            text.text = "Cởi";

        }
        else
        {
            playerInventory.UseSkin(null);
            text.text = "Mặc";
        }
    }

    public override bool PlayAnimation()
    {
        return true;
    }
}
