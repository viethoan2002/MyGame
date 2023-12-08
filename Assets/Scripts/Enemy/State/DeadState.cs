using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    public static event Action AddPoint;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorCtrl enemyAnimatorCtrl)
    {
        enemyManager.GetComponent<Rigidbody>().useGravity = false;
        enemyManager.GetComponent<Collider>().isTrigger = true;
        enemyManager.currentTarget = null;

        if (!enemyManager.isDead)
        {
            enemyManager.isDead = true;
            AddPoint?.Invoke();
            StartCoroutine(AddToPool(enemyManager));
        }
        return this;
    }

    IEnumerator AddToPool(EnemyManager enemyManager)
    {
        yield return new WaitForSeconds(10);
        enemyManager.spawnEnemy.AddPool(enemyManager.transform.gameObject);
        enemyManager.isDead = false;

    }
}
