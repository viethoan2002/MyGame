using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBtn : UseBtn
{
    public override void LoadComponent()
    {
        if (playerInventory.weaponSlotManager.weapon == (WeaponObject)itemSlot.item)
        {
            text.text = "Gỡ";

        }
        else
        {
            text.text = "Trang bị";
        }
    }

    public override void EquipToPlayer()
    {
        if (playerInventory.weaponSlotManager.weapon!=(WeaponObject)itemSlot.item)
        {
            WeaponObject rangedWeapon = (WeaponObject)itemSlot.item;
            playerInventory.UseWeapon(rangedWeapon);
            for (int i = 0; i < displayInventory.inventory.Container.Items.Count; i++)
            {
                if (displayInventory.inventory.Container.Items[i].item.type == ItemType.Weapon)
                {
                    displayInventory.itemsDisplayed[displayInventory.inventory.Container.Items[i]].GetComponentInChildren<WeaponBtn>().text.text = "Trang bị";
                }
            }
            text.text = "Gỡ";

        }
        else
        {
            playerInventory.UseWeapon(null);
            text.text = "Trang bị";
        }   
    }

    public override bool PlayAnimation()
    {
        return true;
    }
}
