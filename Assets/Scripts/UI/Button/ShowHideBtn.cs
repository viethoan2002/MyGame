using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideBtn : BaseBtn
{
    public InventoryManager inventoryManager;
    public GameObject inventoryUI;

    public Sprite imageUp;
    public Sprite imageDown;

    public override void LoadComponent()
    {
    }

    public override void OnClick()
    {
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
            button.image.sprite = imageDown;
        }
        else
        {
            inventoryUI.SetActive(true);
            button.image.sprite = imageUp;
        }
    }
}
