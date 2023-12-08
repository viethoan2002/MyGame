using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int maxStamina;
    public int currentStamina;

    public bool isDead = false;

    public Bar healthBar;
    public Bar staminaBar;
    private PlayerAnimationCtrl playerAnimationCtrl;
    public PlayerInventory playerInventory;

    public static event Action PlayerDead;

    private void Awake()
    {
        playerAnimationCtrl = GetComponentInChildren<PlayerAnimationCtrl>();
        StartCoroutine(DownStamina());
    }

    IEnumerator DownStamina()
    {
        while (true)
        {
            if (currentStamina < maxStamina)
            {
                currentStamina += 1;
                staminaBar.BarFiller((float)currentStamina / maxStamina);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public bool useStamina(int amount)
    {
        if (currentStamina > amount)
        {
            currentStamina -= amount;
            staminaBar.BarFiller((float)currentStamina / maxStamina);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.BarFiller((float)currentHealth / maxHealth);
        currentHealth = currentHealth < 0 ? 0 : currentHealth;
        if (currentHealth == 0)
        {
            OnDead();
        }
    }

    public void Heal(int amount)
    {
        playerInventory.vfxTrigger.OnVfx(0);    
        StartCoroutine(Healing(amount));
    }

    IEnumerator Healing(int amount)
    {
        while (amount > 0)
        {
            currentHealth += 1;
            amount -= 1;
            healthBar.BarFiller((float)currentHealth / maxHealth);
            if (currentHealth > 100)
            {
                currentHealth = 100;
                amount = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }

        playerInventory.vfxTrigger.OffVfx(0);
    }

    private void OnDead()
    {
        playerAnimationCtrl.PlayerTargetAnimation("Dead", true);
        isDead = true;
        PlayerDead?.Invoke();
    }
}
