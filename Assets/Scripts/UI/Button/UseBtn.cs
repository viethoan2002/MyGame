using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UseBtn : BaseBtn
{
    public ItemSlot itemSlot;
    public DisplayInventory displayInventory;
    public PlayerInventory playerInventory;
    public Image image;
    public TMP_Text text;
    public float timeFill;

    bool isClick;

    public void SetInfor(ItemSlot _itemSlot,DisplayInventory _displayInventory,PlayerInventory _playerInventory)
    {
        if (_playerInventory == null)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            itemSlot = _itemSlot;
            displayInventory = _displayInventory;
            playerInventory = _playerInventory;
        }    
    }
    public override void LoadComponent() { }

    public override void OnClick() 
    {
        //if (!isClick)
        //{      
        //    if(PlayAnimation())
        //    {
        //        isClick = true;
        //        StartCoroutine(FillImage());
        //    }    
        //}
        if (PlayAnimation())
        {
            EquipToPlayer();
        }
    }

    public IEnumerator FillImage()
    {
        float elapsedTime = 0f;

        while (elapsedTime < timeFill)
        {
            elapsedTime += Time.fixedDeltaTime;
            float fillPercentage = elapsedTime / timeFill;
            image.fillAmount = fillPercentage;

            yield return null;
        }

        image.fillAmount = 0;
        EquipToPlayer();
        isClick = false;
    }

    public abstract bool PlayAnimation();
    public abstract void EquipToPlayer();
}
