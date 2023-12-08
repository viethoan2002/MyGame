using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemyDisplay : MonoBehaviour
{
    public List<SkinnedMeshRenderer> listSkin = new List<SkinnedMeshRenderer>();
    public Material outline;

    public void OpenOutLine()
    { 
        for (int i = 0; i < listSkin.Count; i++)
        {
            Material[] newMaterials = listSkin[i].materials;
            newMaterials[1] = outline;
            listSkin[i].materials = newMaterials;
        }
    }

    public void CloseOutLine()
    {
        for (int i = 0; i < listSkin.Count; i++)
        {
            Material[] newMaterials = listSkin[i].materials;
            newMaterials[1] = null;
            listSkin[i].materials = newMaterials;
        }
    }
}
