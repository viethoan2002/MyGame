using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public IdleState idleState;
    public AttackState attackState;
    public PusureTargetState pursureTargetState;
    public DeadState deadState;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorCtrl enemyAnimatorCtrl)
    {
        if (enemyStats.currentHealth == 0)
        {
            return deadState;
        }


        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        if (enemyManager.isPreformingAction)
        {
            enemyAnimatorCtrl.anm.SetFloat("velocity", 0, 0.1f, Time.deltaTime);
        }

        if (enemyManager.currentRecoveryTime <= 0 && distanceFromTarget <= enemyManager.maximumAttackRange)
        {
            return attackState;
        }
        else if (distanceFromTarget > enemyManager.maximumAttackRange)
        {
            return pursureTargetState;
        }
        else
        {
            return this;
        }
    }
}
