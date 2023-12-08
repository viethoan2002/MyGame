using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;

public class PusureTargetState : State
{
    public IdleState idleState;

    public CombatStanceState combatStanceState;

    public DeadState deadState;

    public Transform transformUI;

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorCtrl enemyAnimator)
    {
        if (enemyStats.currentHealth == 0)
        {
            return deadState;
        }

        if (enemyStats.currentHealth == 0)
        {
            return idleState;
        }

        if (enemyManager.isPreformingAction)
        {
            enemyAnimator.anm.SetFloat("velocity", 0, 0.1f, Time.deltaTime);
            return this;
        }

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

        if (distanceFromTarget > enemyManager.maximumDistanceTarget)
        {
            enemyAnimator.anm.SetFloat("velocity", 0, 0.1f, Time.deltaTime);
            return this;
        }

        if (distanceFromTarget > enemyManager.maximumAttackRange)
        {
            enemyAnimator.anm.SetFloat("velocity", 1, 0.1f, Time.deltaTime);
        }

        HandleRotateTowardsTarget(enemyManager);
        enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
        enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;

        if (distanceFromTarget <= enemyManager.maximumAttackRange)
        {
            return combatStanceState;
        }
        else
        {
            return this;
        }
    }

    private void HandleRotateTowardsTarget(EnemyManager enemyManager)
    {
        if (enemyManager.isPreformingAction)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRb.velocity;

            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRb.velocity = relativeDirection;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
        }

        float yAngle = enemyManager.transform.rotation.y;
        transformUI.rotation = Quaternion.Euler(0, -yAngle, 0);
    }
}
