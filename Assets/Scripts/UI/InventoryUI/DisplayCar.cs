using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCar : MonoBehaviour
{
    public static DisplayCar Instance;

    public GameObject playerUI;
    public GameObject content;
    public GameObject needle;
    public Image carHealthUI;
    public Image carPetrolUI;

    private void Awake()
    {
        if (DisplayCar.Instance == null)
        {
            DisplayCar.Instance = this;
        }
    }

    public void HideUI()
    {
        content.SetActive(false);
        playerUI.SetActive(true);
    }

    public void ShowUI()
    {
        content.SetActive(true);
        playerUI.SetActive(false);
    }

    public void setCarHealthUI(float amount)
    {
        carHealthUI.fillAmount = amount;
    }

    public void setCarPetrolUI(float amount)
    {
        carPetrolUI.fillAmount = amount;
    }

    public void setCarSpeedUI(float amount)
    {
        amount = amount > 1 ? 1 : amount;
        amount = amount < 0 ? 0 : amount;
        //quay từ goc -2 đến góc -178
        float angle = -2 - 176 * amount;
        needle.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
