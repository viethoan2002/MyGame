using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Image filler;

    public void BarFiller(float amount)
    {
        filler.fillAmount = amount;
    }
}
