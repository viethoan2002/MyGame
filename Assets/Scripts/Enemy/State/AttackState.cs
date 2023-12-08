using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public DeadState deadState;
    public CombatStanceState combatStanceState;
    public PusureTargetState pusureTargetState;
    public float minimumDistanceNeededToAttack;
    public float maximumAttackAngle;
    public float minimumAttackAngle;
    public float recoveryTime;

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorCtrl enemyAnimatorCtrl)
    {
        if (enemyStats.currentHealth == 0)
        {
            return deadState;
        }

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        if (enemyManager.isPreformingAction)
            return combatStanceState;


        if (distanceFromTarget < minimumDistanceNeededToAttack)
        {
            return this;
        }

        else if (distanceFromTarget < maximumAttackAngle)
        {
            if (viewableAngle <= maximumAttackAngle
                && viewableAngle >= minimumAttackAngle)
            {
                if (enemyManager.currentRecoveryTime <= 0 && enemyManager.isPreformingAction == false)
                {
                    enemyAnimatorCtrl.anm.SetFloat("velocity", 0, 0.1f, Time.deltaTime);
                    enemyAnimatorCtrl.PlayerTargetAnimation("Zombie Attack", true);
                    enemyManager.isPreformingAction = true;
                    enemyManager.currentRecoveryTime = recoveryTime;
                    return combatStanceState;
                }
            }
            else
                return pusureTargetState;
        }

        return combatStanceState;
    }
}
