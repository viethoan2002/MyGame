using UnityEngine;

public class IdleState : State
{
    public PusureTargetState pusureTargetState;
    public DeadState deadState;

    public LayerMask detectionLayer;

    public Collider[] colliders;

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorCtrl enemyAnimatorCtrl)
    {
        if (enemyStats.currentHealth == 0)
        {
            return deadState;
        }

        #region Enemy Target Detection
        colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            PlayerStats characterStats = colliders[i].transform.GetComponent<PlayerStats>();

            if (characterStats != null)
            {
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    enemyManager.currentTarget =characterStats;
                }
            }
        }

        if (enemyManager.currentTarget != null)
        {
            return pusureTargetState;
        }
        else
        {
            return this;
        }
        #endregion
    }
}
