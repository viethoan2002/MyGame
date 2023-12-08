using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject,ISerializationCallbackReceiver
{
    public string nameInventory;
    public string savePath;
    private ItemDatatbaseObject database;
    public Inventory Container = new Inventory();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatatbaseObject)AssetDatabase.LoadAssetAtPath($"Assets/Resources/Database.asset", typeof(ItemDatatbaseObject));
#else     
        database = Resources.Load<ItemDatatbaseObject>("Database");
#endif  
    }

    public bool AddItem(ItemObject _item,int _amount)
    {
        if (Container.cabinet >= _item.weight * _amount)
        {
            for (int i = 0; i < Container.Items.Count; i++)
            {
                if (Container.Items[i].item == _item)
                {
                    Container.Items[i].AddAmount(_amount);
                    Container.cabinet -= _item.weight * _amount;
                    return true;
                }
            }
            Container.cabinet -= _item.weight * _amount;
            Container.Items.Add(new ItemSlot(database.GetID[_item], _item, _amount));
            return true;
        }
        else
        {
            return false;
        } 
    }

    public void RemoveItem(ItemObject _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].RemoveAmount(_amount);
                Container.cabinet += _item.weight * _amount;
                if (Container.Items[i].amount <= 0)
                {
                    Container.Items.Remove(Container.Items[i]);
                }
                return;
            }       
        }
    }
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath,savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();
        }
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Items.Count; i++)
        {
            Container.Items[i].item = database.GetItem[Container.Items[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {

    }
}


[System.Serializable]
public class Inventory
{
    public float cabinet;
    public List<ItemSlot> Items = new List<ItemSlot>();
}


