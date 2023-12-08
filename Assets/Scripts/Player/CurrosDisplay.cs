using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrosDisplay : MonoBehaviour
{
    public List<Renderer> listRender = new List<Renderer>();
    public void OpenOutLine()
    {
        for (int i = 0; i < listRender.Count; i++)
        {
            listRender[i].material.color = Color.green;
        }
    }

    public void CloseOutLine()
    {
        for (int i = 0; i < listRender.Count; i++)
        {

            listRender[i].material.color = Color.white;
        }
    }
}
