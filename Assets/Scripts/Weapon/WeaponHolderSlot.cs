using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolderSlot : MonoBehaviour
{
    public Transform parentOverride;
    public GameObject currentWeaponModel;
    public PlayerDamageCollider playerDamageCollider;
    public Image image;

    private int damage;
    public void UnLoadWeapon()
    {
        if (currentWeaponModel != null)
        {
            currentWeaponModel.SetActive(false);
        }
    }

    public void UnLoadWeaponAndDestroy()
    {
        if (currentWeaponModel != null)
        {
            playerDamageCollider = null;
            Destroy(currentWeaponModel);
        }
    }

    public void LoadWeaponModel(WeaponObject weapon)
    {
        UnLoadWeaponAndDestroy();

        if (weapon == null)
        {
            UnLoadWeapon();
            return;
        }

        GameObject model = Instantiate(weapon.prefab) as GameObject;
        playerDamageCollider = model.GetComponentInChildren<PlayerDamageCollider>();
        damage = weapon.atkBonus;
        if (model != null)
        {
            if (weapon.isRange)
            {
                model.GetComponentInChildren<BulletManager>().weaponObject = weapon;
                model.GetComponentInChildren<BulletManager>().image = image;
            }
               
            model.transform.parent = parentOverride;
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
        }

        currentWeaponModel = model;
    }

    public void ShowWeapon()
    {
        if(currentWeaponModel!=null)
            currentWeaponModel.SetActive(true);
    }

    public void HideWeapon()
    {
        Debug.Log("kk");
        if (currentWeaponModel != null)
            currentWeaponModel.SetActive(false);
    }

    public void OpenDamageCollier()
    {
        playerDamageCollider.OpenDamageCollider(damage);
    }
   
    public void CloseDamageCollider()
    {
        playerDamageCollider.CloseDamageCollider();
    }
}
