using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    public WeaponObject weaponObject;
    public ParticleSystem particle;
    public bool reLoad = false;

    public Image image;

    public bool ShootBullet()
    {
        if (weaponObject.currentBullet > 0)
        {
            return true;
        }
        else
        {
            if (!reLoad)
            {
                reLoad = true;
                StartCoroutine(Reload());
            }

            return false;
        }
    }

    public IEnumerator Reload()
    {
        float elapsedTime = 0f;

        while (elapsedTime < weaponObject.timeReload)
        {
            elapsedTime += Time.fixedDeltaTime;
            float fillPercentage = elapsedTime / weaponObject.timeReload;
            image.fillAmount = fillPercentage;

            yield return null;
        }

        weaponObject.currentBullet = weaponObject.maxBullet;
        image.fillAmount = 0;
        reLoad = false;
    }

    public void RemoveBullet()
    {
        weaponObject.currentBullet -= 1;
    }

    //public int ReloadBullet(int amount)
    //{
    //    if (weaponObject.currentBullet + amount > weaponObject.maxBullet)
    //    {
    //        int bulletth = weaponObject.maxBullet - weaponObject.currentBullet;
    //        weaponObject.currentBullet = weaponObject.maxBullet;
    //        return bulletth;
    //    }
    //    else
    //    {
    //        weaponObject.currentBullet += amount;
    //        return amount;
    //    }
    //}
}
