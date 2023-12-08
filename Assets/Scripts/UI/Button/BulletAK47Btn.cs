using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAK47Btn : UseBtn
{
    public override void EquipToPlayer()
    {
        WeaponObject weapon = playerInventory.weaponSlotManager.weapon;
        if (weapon.name == "Ak47")
        {
            BulletManager bulletManager = playerInventory.GetComponentInChildren<BulletManager>();
          //  int amount = bulletManager.ReloadBullet(35);
            displayInventory.RemoveItem(itemSlot.item, 1);
        }
    }

    public override bool PlayAnimation()
    {
        WeaponObject weapon = playerInventory.weaponSlotManager.weapon;

        if (weapon.name == "Ak47")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}