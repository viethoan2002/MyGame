using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolBtn : UseBtn
{
    public override void EquipToPlayer()
    {
        if (playerInventory.UsePetrol())
        {
            displayInventory.RemoveItem(itemSlot.item, 1);
            playerInventory.playerAnimationCtrl.PlayerTargetAnimation("player_kneeling_up", true);
        }
    }

    public override bool PlayAnimation()
    {
        if (playerInventory.carController == null)
        {
            return false;
        }
        else
        {
            playerInventory.playerAnimationCtrl.PlayerTargetAnimation("player_kneeling_down", true);
            playerInventory.transform.LookAt(playerInventory.carController.transform);
            return true;
        }
    }
}
