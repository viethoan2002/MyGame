using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    private EnemyAnimatorCtrl enemyAnimatorCtrl;

    public EnemyHealthBar enemyHealthBar;

    private void Awake()
    {
        enemyAnimatorCtrl = GetComponentInChildren<EnemyAnimatorCtrl>();
    }

    private void Start()
    {
        transform.gameObject.layer = LayerMask.NameToLayer("Enemy");
        currentHealth = maxHealth;
        enemyHealthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = currentHealth < 0 ? 0 : currentHealth;

        enemyHealthBar.gameObject.SetActive(true);
        enemyHealthBar.SetCurrentHealth(currentHealth);

        if(currentHealth == 0)
        {
            OnDead();
        }
        else
        {
            enemyAnimatorCtrl.PlayerTargetAnimation("Zombie Take Damage", true);
        }
    }

    private void OnDead()
    {
        transform.gameObject.layer = 0;
        enemyAnimatorCtrl.PlayerTargetAnimation("Zombie Dying", true);
    }
}
