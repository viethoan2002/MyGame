using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public CarLocomotion carLocomotion;
    public CarStats carStats;
    public CarSound carSound;

    public CheckPosition checkRight;
    public CheckPosition checkLeft;
    public CheckPosition checkBack;

    public GameObject bodyCar;

    public bool isControl=false;

    private void Start()
    {
        carSound = GetComponentInChildren<CarSound>();
        carLocomotion = GetComponent<CarLocomotion>();
        carStats = GetComponentInChildren<CarStats>();
    }

    private void Update()
    {
        if((checkLeft.isPlayerHere || checkRight.isPlayerHere))
        {
            if (checkLeft.Player != null)
            {
                checkLeft.Player.transform.GetComponent<PlayerInventory>().carController = this;
            }
            else
            {
                checkRight.Player.transform.GetComponent<PlayerInventory>().carController = this;
            }
        }

        if((checkLeft.isPlayerHere || checkRight.isPlayerHere) && InputHandle.Instance.use)
        {
            carSound.SoundInCar();

            if (checkLeft.Player != null)
            {
                if (checkLeft.Player.activeSelf)
                {
                    checkLeft.Player.transform.SetParent(transform);
                    checkLeft.Player.SetActive(false);
                    checkLeft.SetRender(false);
                    bodyCar.layer = LayerMask.NameToLayer("Character");
                    carLocomotion.enabled = true;
                    ShowCarUI();
                }
                else
                {
                    checkLeft.Player.transform.SetParent(null);
                    checkLeft.Player.SetActive(true);
                    checkLeft.SetRender(true);
                    bodyCar.layer = LayerMask.NameToLayer("Default");
                    carLocomotion.enabled = false;
                    HideCarUI();
                }
            }
            else
            {
                if (checkRight.Player.activeSelf)
                {
                    checkRight.Player.transform.SetParent(transform);
                    checkRight.Player.SetActive(false);
                    checkRight.SetRender(false);
                    bodyCar.layer = LayerMask.NameToLayer("Character");
                    carLocomotion.enabled = true;
                    ShowCarUI();
                }
                else
                {
                    checkRight.Player.transform.SetParent(null);
                    checkRight.Player.SetActive(true);
                    checkRight.SetRender(true);
                    bodyCar.layer = LayerMask.NameToLayer("Default");
                    carLocomotion.enabled = false;
                    HideCarUI();
                }
            }

            
        }

    }

    public void ShowCarUI()
    {
        DisplayCar.Instance.ShowUI();
        DisplayCar.Instance.setCarHealthUI(carStats.currentHealth / carStats.maxHealth);
        DisplayCar.Instance.setCarPetrolUI(carStats.currentPetrol / carStats.maxPetrol);
    }

    public void HideCarUI()
    {
        DisplayCar.Instance.HideUI();
    }

    public void AddPetrol()
    {
        carStats.AddPetrol();
    }
}
