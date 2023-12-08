using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]
public class WeaponObject : ItemObject
{
    public bool isRange;
    public int atkBonus;
    public AudioClip SoundAttack;
    public AudioClip SoundCantAttack;
    public int maxBullet;
    public int currentBullet;
    public int timeReload;
    public float speedFire;

    private void Awake()
    {
        type = ItemType.Weapon;
    }
}
