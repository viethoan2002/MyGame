using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    EnemyAnimatorCtrl enemyAnimatorCtrl;
    public EnemyStats enemyStats;

    public SpawnEnemy spawnEnemy;

    public State idleState;
    public State currentState;
    public PlayerStats currentTarget;
    public NavMeshAgent navMeshAgent;
    public Rigidbody enemyRb;

    public bool isPreformingAction;
    public float distanceFromTarget;
    public float rotationSpeed = 15;
    public float maximumAttackRange = 1.5F;

    [Header("A.I Settings")]
    public float detectionRadius = 20;

    public float maximumDistanceTarget = 50;
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;
    public float currentRecoveryTime = 0;

    public bool isDead;

    private void OnEnable()
    {
        DayNightCycle.OnDay += SetDefaultEnemy;
        DayNightCycle.OnNight += SetScaryEnemy;
    }

    private void OnDisable()
    {
        DayNightCycle.OnDay -= SetDefaultEnemy;
        DayNightCycle.OnNight -= SetScaryEnemy;
    }


    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyStats = GetComponent<EnemyStats>();
        enemyAnimatorCtrl = GetComponentInChildren<EnemyAnimatorCtrl>();
    }
    private void Start()
    {
        enemyRb.isKinematic = false;
    }

    private void Update()
    {
        HandleRecoveryTime();
        HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimatorCtrl);

            if (nextState != null)
            {
                currentState = nextState;
            }
        }
    }

    private void HandleRecoveryTime()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPreformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPreformingAction = false;
            }
        }
    }

    public void SetDefaultEnemy()
    {
        maximumDetectionAngle = 50;
        minimumDetectionAngle = -50;
        navMeshAgent.speed = 3.5f;
        maximumDistanceTarget = 50;
        detectionRadius = 20;
    }

    public void SetScaryEnemy()
    {
        maximumDetectionAngle = 180;
        minimumDetectionAngle = -180;
        navMeshAgent.speed = 5;
        maximumDistanceTarget = 100;
        detectionRadius = 40;
    }
}
