using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public PlayerStats playerStats;
    public WeaponSlotManager weaponSlotManager;
    public PlayerSkinManager playerSkin;
    public PlayerAnimationCtrl playerAnimationCtrl;
    public PlayerSound playerSound;
    public VfxTrigger vfxTrigger;

    public CarController carController;
    public AnimatorOverrideController rangeWeaponAnm;
    public AnimatorOverrideController meleeWeaponAnm;

    private void Awake()
    {
        inventory.Load();
    }

    private void Start()
    {
        LoadPlayer();

        playerStats = GetComponent<PlayerStats>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        playerSkin = GetComponentInChildren<PlayerSkinManager>();
        playerAnimationCtrl = GetComponentInChildren<PlayerAnimationCtrl>();
        playerSound = GetComponentInChildren<PlayerSound>();
    }

    private void Update()
    {
        weaponSlotManager.CheckWeapon();
        playerSkin.CheckSkin();
    }

    public void UseWeapon(WeaponObject weapon)
    {
        weaponSlotManager.EquipWeapon(weapon);

        if (weapon == null)
            return;

        if (weapon.isRange)
        {
            playerAnimationCtrl.animator.runtimeAnimatorController = rangeWeaponAnm;
        }
        else
        {
            playerAnimationCtrl.animator.runtimeAnimatorController = meleeWeaponAnm;
        }

        
    }

    public void UseSkin(EquipmentObject equipment)
    {
        playerSkin.EquipSkin(equipment);
    }

    public void Health(int amount)
    {
        playerStats.Heal(amount);
    }

    public bool UsePetrol()
    {
        if (carController != null)
        {
            carController.AddPetrol();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveLoadManager.Instance.LoadPlayer();
        if (data != null)
        {
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            transform.position = position;
            transform.rotation = Quaternion.Euler(0, data.angleY, 0);

            for(int i = 0; i < inventory.Container.Items.Count; i++)
            {
                if (inventory.Container.Items[i].ID == data.idWeapon)
                {
                    UseWeapon((WeaponObject)inventory.Container.Items[i].item);
                }

                if(inventory.Container.Items[i].ID == data.idSkin)
                {
                    UseSkin((EquipmentObject)inventory.Container.Items[i].item);
                }
            }
        }
        else
        {
            Debug.Log("data null");
        }


    }

    public void SavePlayer(Vector3 pos,float angleY)
    {
        int idWeapon = 0;
        int idSkin = 0;

        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            if (inventory.Container.Items[i].item == (ItemObject)weaponSlotManager.weapon)
            {
                idWeapon = inventory.Container.Items[i].ID;
            }

            if (inventory.Container.Items[i].item == (ItemObject)playerSkin.equipment)
            {
                idSkin = inventory.Container.Items[i].ID;
            }
        }
        SaveLoadManager.Instance.SavePlayer(pos, angleY, idWeapon, idSkin);
    }
}
