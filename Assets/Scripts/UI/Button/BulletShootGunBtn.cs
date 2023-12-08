using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootGunBtn : UseBtn
{
    public override void EquipToPlayer()
    {
        WeaponObject weapon = playerInventory.weaponSlotManager.weapon;
        if (weapon.name == "Shootgun" || weapon.name == "Rifle")
        {
            BulletManager bulletManager = playerInventory.GetComponentInChildren<BulletManager>();
           // int amount = bulletManager.ReloadBullet(itemSlot.amount);
          //  displayInventory.RemoveItem(itemSlot.item, amount);
        } 
    }

    public override bool PlayAnimation()
    {
        WeaponObject weapon = playerInventory.weaponSlotManager.weapon;

        if (weapon.name == "Shootgun" || weapon.name == "Rifle")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
