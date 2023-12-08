using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
        maxHealth = 100;
    }

    public void HealSelf(int health)
    {

    }

    public void TakeDamage(int damage)
    {
    }
}
