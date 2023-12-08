using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkinManager : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    public InventoryObject inventory;
    public EquipmentObject equipment;
    public CharacterInventory characterInventory;

    [SerializeField]
    private EquipmentObject defaultSkin;

    private void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        inventory = GetComponentInParent<PlayerInventory>().inventory;
    }

    public void CheckSkin()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            if (inventory.Container.Items[i].item == equipment)
                return;
        }

        EquipSkin(null);
    }

    public void EquipSkin(EquipmentObject _equipment)
    {       
        equipment = _equipment;

        if (equipment != null)
        {
            skinnedMeshRenderer.sharedMesh = equipment.skin;
            characterInventory.SetSkinUI(_equipment);
        }
        else
        {
            skinnedMeshRenderer.sharedMesh = defaultSkin.skin;
            characterInventory.SetSkinUI(defaultSkin);
        }
    }
}
