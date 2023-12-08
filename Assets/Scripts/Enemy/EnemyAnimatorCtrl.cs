using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorCtrl : MonoBehaviour
{
    public Animator anm;
    public EnemyManager enemyManager;

    private void Awake()
    {
        anm = GetComponent<Animator>();
        enemyManager = GetComponentInParent<EnemyManager>();
    }

    public void PlayerTargetAnimation(string targetAnim, bool isTnteracting)
    {
        anm.applyRootMotion = isTnteracting;
        anm.SetBool("isInteracting", isTnteracting);
        anm.CrossFade(targetAnim, 0.1f);
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        enemyManager.enemyRb.drag = 0;
        Vector3 deltaPosition = anm.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyManager.enemyRb.velocity = velocity;
    }
}
