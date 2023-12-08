using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image iconItem;
    public Image backGround;
    public TMP_Text nameItem;
    public TMP_Text typeItem;

    public void SetDetailItem(ItemSlot _item)
    {
        iconItem.sprite = _item.item.icon;
        nameItem.text = _item.item.nameItem + "(" + _item.amount + ")";
        typeItem.text = _item.item.type.ToString();

    }

    public void BackGroundEnter()
    {
        backGround.color = new Color(0.2143965f, 0.6886792f, 0.178017f, 0.5f);
    }

    public void BackGroundExit()
    {
        backGround.color = new Color(0.2196079f, 0.2196079f, 0.2196079f,1);
    }
}
