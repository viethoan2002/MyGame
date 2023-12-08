using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public PlayerInventory playerInventory;
    public RectTransform targetInventory;

    public Transform grid;
    public GameObject prefabInventorySlot;
    public GameObject prefabDrag;
    public GameObject inventoryUI;
    public ItemObject itemDrag;

    [Header("UI")]
    public TMP_Text nameInventory;
    public TMP_Text containerInventory;

    private List<ItemSlot> itemNotInInventory = new List<ItemSlot>();

    public Dictionary<ItemSlot, GameObject> itemsDisplayed = new Dictionary<ItemSlot, GameObject>();

    private void Start()
    {
        prefabDrag.SetActive(false);
    }

    private void Update()
    {
        if(inventory!=null)
            UpdateDisplay();
        else
        {
            ClearInventory();
        }
    }

    public void SetInventory(InventoryObject _inventory)
    {
        this.inventory = _inventory;
        if (_inventory != null)
        {
            inventoryUI.SetActive(true);
            nameInventory.text = _inventory.nameInventory;
        }
        else
        {
            nameInventory.text = "Floor";
        }
    }

    private void UpdateDisplay()
    {
        foreach(var _itemDP in itemsDisplayed)
        {
            if (!inventory.Container.Items.Contains(_itemDP.Key))
            {

                itemNotInInventory.Add(_itemDP.Key);
            }
        }

        for(int i = 0; i < itemNotInInventory.Count; i++)
        {
            Destroy(itemsDisplayed[itemNotInInventory[i]]);
            itemsDisplayed.Remove(itemNotInInventory[i]);
        }

        itemNotInInventory.Clear();

        for(int i = 0; i < inventory.Container.Items.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container.Items[i]))
            {
                itemsDisplayed[inventory.Container.Items[i]].GetComponent<InventorySlot>().SetDetailItem(inventory.Container.Items[i]);
            }
            else
            {
                var obj = Instantiate(inventory.Container.Items[i].item.prefabUI, Vector3.zero, Quaternion.identity, grid);
                obj.SetActive(true);
                obj.GetComponent<InventorySlot>().SetDetailItem(inventory.Container.Items[i]);
                obj.GetComponentInChildren<UseBtn>().SetInfor(inventory.Container.Items[i], this, playerInventory);

                ItemObject _i = inventory.Container.Items[i].item;

                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj, _i); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
                itemsDisplayed.Add(inventory.Container.Items[i], obj);
            }
        }
    }

    private void ClearInventory()
    {
       foreach(var inventory in itemsDisplayed)
       {
            Destroy(inventory.Value);
       }
        itemsDisplayed.Clear();
        itemNotInInventory.Clear();
        inventoryUI.SetActive(false);
    }

    private void AddEvent(GameObject obj,EventTriggerType type,UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrriger = new EventTrigger.Entry();
        eventTrriger.eventID = type;
        eventTrriger.callback.AddListener(action);
        trigger.triggers.Add(eventTrriger);
    }

    public void OnEnter(GameObject obj)
    {
        //obj.GetComponentInChildren<InventorySlot>().BackGroundEnter();
    }

    private void OnExit(GameObject obj)
    {
        obj.GetComponentInChildren<InventorySlot>().BackGroundExit();
        itemDrag = null;
    }

    private void OnDragStart(GameObject obj)
    {
        prefabDrag.SetActive(true);
        prefabDrag.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    private void OnDragEnd(GameObject obj,ItemObject _item)
    {
        bool isMouseOverPanel = RectTransformUtility.RectangleContainsScreenPoint(targetInventory, Input.mousePosition);

        if (isMouseOverPanel && targetInventory.GetComponentInParent<DisplayInventory>().inventory!=null 
            && targetInventory.gameObject.activeSelf == true)
        {
            if(targetInventory.GetComponentInParent<DisplayInventory>().AddItem(_item, 1))
            {
                RemoveItem(_item, 1);
            }
        }

        itemDrag = null;
        obj.GetComponentInChildren<InventorySlot>().BackGroundExit();
        prefabDrag.SetActive(false);
    }

    private void OnDrag(GameObject obj)
    {
        prefabDrag.GetComponent<RectTransform>().position = Input.mousePosition;
        obj.GetComponentInChildren<InventorySlot>().BackGroundEnter();
    }

    public bool AddItem(ItemObject _item, int _amount)
    {
       return inventory.AddItem(_item, _amount);
    }

    public void RemoveItem(ItemObject _item, int _amount)
    {

        inventory.RemoveItem(_item, _amount);
    }
}
