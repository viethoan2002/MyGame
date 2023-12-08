using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    public Image weaponUI;
    public Image skinUI;
    public Animator animator;

    public Transform gunSlot;
    public Transform weaponSlot;

    public GameObject weaponModel;
    public SkinnedMeshRenderer SkinnedMeshRenderer;

    int weaponIndex = 0;

    private void OnEnable()
    {
        animator.SetFloat("weapon", weaponIndex);
    }

    public void SetWeaponUI(WeaponObject weapon)
    {
        if (weapon != null)
        {
            Destroy(weaponModel);
            weaponModel = Instantiate(weapon.prefab) as GameObject;
            if (weapon.isRange)
            {
                weaponIndex = 1;
                animator.SetFloat("weapon", 1);
                weaponModel.transform.parent = gunSlot;
            }  
            else
            {
                weaponIndex = 2;
                animator.SetFloat("weapon", 2);
                weaponModel.transform.parent = weaponSlot;
            }

           
            weaponModel.transform.localPosition = Vector3.zero;
            weaponModel.transform.localRotation = Quaternion.identity;
            weaponModel.transform.localScale = Vector3.one;

            weaponUI.sprite = weapon.icon;
            Color _color = weaponUI.color;
            _color.a = 1;
            weaponUI.color = _color;
        }
        else
        {
            weaponIndex = 0;
            animator.SetFloat("weapon", 0);
            Destroy(weaponModel);
            weaponUI.sprite = null;
            Color _color = weaponUI.color;
            _color.a = 0;
            weaponUI.color = _color;
        }
    }

    public void SetSkinUI(EquipmentObject equipment)
    {
        SkinnedMeshRenderer.sharedMesh = equipment.skin;

        if (equipment.icon != null)
        {
            skinUI.sprite = equipment.icon;
            Color _color = skinUI.color;
            _color.a = 1;
            skinUI.color = _color;
        }
        else
        {
            skinUI.sprite = null;
            Color _color = skinUI.color;
            _color.a = 0;
            skinUI.color = _color;
        }
    }
}
