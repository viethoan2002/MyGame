using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayGun : MonoBehaviour
{
    public WeaponObject gun;
    public TMP_Text nameGun;
    public TMP_Text currentBullet;
    public TMP_Text maxBullet;

    public void SetGun(WeaponObject _gun)
    {
        this.gun = _gun;
    }

    private void Update()
    {
        nameGun.text = gun.name;
        currentBullet.text = $"{gun.currentBullet:00}";
        maxBullet.text = $"{gun.maxBullet:00}";
    }
}
