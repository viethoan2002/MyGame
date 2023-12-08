using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;
    public InventoryList inventoryList;

    private void Awake()
    {
        if (SaveLoadManager.Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        LoadAllInventorys();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveAllInventorys();
        }
    }

    public void LoadAllInventorys()
    {
        foreach (InventoryObject obj in inventoryList.inventoryList)
        {
            obj.Load();
        }
    }

    public void SaveAllInventorys()
    {
        foreach (InventoryObject obj in inventoryList.inventoryList)
        {
            obj.Save();
        }
    }

    public void SavePlayer(Vector3 pos,float angle,int idWeapon,int idSkin)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(pos,angle,idWeapon,idSkin);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save filr not found int" + path);
            return null;
        }
    }

    private void OnApplicationQuit()
    {
        foreach (InventoryObject obj in inventoryList.inventoryList)
        {
            obj.Container.Items.Clear();
        }
    }
}
