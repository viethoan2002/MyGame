using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxTrigger : MonoBehaviour
{
    public List<GameObject> Vfxs;

    public void OnVfx(int index)
    {
        Vfxs[index].SetActive(true);
    }

    public void OffVfx(int index)
    {
        Vfxs[index].SetActive(false);
    }
}
