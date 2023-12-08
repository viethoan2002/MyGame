using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDayNight : MonoBehaviour
{
    public Sprite iconDay;
    public Sprite iconNight;

    public Image iconDayNight;
    public Image bgDayNight;
    public Image filler;

    public void SetDayNight(bool isDay)
    {
        if (isDay)
        {
            iconDayNight.sprite = iconDay;
            bgDayNight.color = Color.white;
            filler.rectTransform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            iconDayNight.sprite = iconNight;
            bgDayNight.color = Color.black;
            filler.rectTransform.rotation = Quaternion.Euler(180, 0, 0);
        }    
    }

    public void Filler(float amount)
    {
        filler.fillAmount = amount;
    }
}
