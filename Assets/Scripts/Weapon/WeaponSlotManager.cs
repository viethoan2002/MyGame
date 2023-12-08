using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotManager : MonoBehaviour
{
    private WeaponHolderSlot weaponHolderSlot;
    private PlayerAnimationCtrl playerAnimationCtrl;
    public InventoryObject inventory;
    public CharacterInventory characterInventory;
    public GameObject gunUI;

    public WeaponObject weapon;

    private void Awake()
    {
        weaponHolderSlot = GetComponentInChildren<WeaponHolderSlot>();
        playerAnimationCtrl = GetComponentInChildren<PlayerAnimationCtrl>();
        inventory = GetComponent<PlayerInventory>().inventory;
    }

    public void CheckWeapon()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            if (inventory.Container.Items[i].item == weapon)
            {
                playerAnimationCtrl.animator.SetFloat("weapon", 1);
                return;
            }
          
        }

        playerAnimationCtrl.animator.SetFloat("weapon", 0);
        EquipWeapon(null);
    }

    public void EquipWeapon(WeaponObject _weapon)
    {
        characterInventory.SetWeaponUI(_weapon);   
        weapon = _weapon;
        weaponHolderSlot.LoadWeaponModel(weapon);
        
        if(_weapon != null)
        {
            if (_weapon.isRange)
            {
                gunUI.SetActive(true);
                gunUI.GetComponent<DisplayGun>().SetGun(_weapon);
            }  
        }
        else
        {
            gunUI.SetActive(false);
        }
    }
}
