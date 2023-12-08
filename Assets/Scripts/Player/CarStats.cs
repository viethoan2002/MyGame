using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStats : CharacterStats
{
    public float maxPetrol;
    public float currentPetrol;
    public bool isMove;

    private void LateUpdate()
    {
        RemovePetrol();
    }

    public void AddPetrol()
    {
        currentPetrol = maxPetrol;
    }

    public void RemovePetrol()
    {
        if (isMove)
        {
            currentPetrol -= 1 * Time.fixedDeltaTime;
            if (currentPetrol < 0)
            {
                currentPetrol = 0;
            }

            DisplayCar.Instance.setCarPetrolUI(currentPetrol / maxPetrol);
        }
    }
}
