using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationCtrl : MonoBehaviour
{
    public Animator animator;

    public PlayerLocomotion playerLocomotion;

    public AnimatorOverrideController gunAnm;

    public AnimatorOverrideController weaponAnm;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayerTargetAnimation(string targetAnim, bool isTnteracting)
    {
        animator.applyRootMotion = isTnteracting;
        animator.SetBool("isInteracting", isTnteracting);
        animator.CrossFade(targetAnim, 0.1f);
    }

    public void UpdateValueAnimation(float velocity,float angle,Vector3 crossProduct)
    {
        animator.SetFloat("velocity", velocity);

        if (velocity > 0.1)
        { 
            if (angle < 22.5)
            {
                //Debug.Log("forward");
                animator.SetFloat("angle", 1);
            }

            if (angle > 22.5 && angle < 67.5)
            {
                if (crossProduct.y > 0)
                {
                    //Debug.Log("forward_left");
                    animator.SetFloat("angle", 2);
                }
                else if (crossProduct.y < 0)
                {
                    //Debug.Log("forward_right");
                    animator.SetFloat("angle", 8);
                }
            }

            if (angle > 67.5 && angle < 112.5)
            {
                if (crossProduct.y > 0)
                {
                    //Debug.Log("left");
                    animator.SetFloat("angle", 3);
                }
                else if (crossProduct.y < 0)
                {
                    //Debug.Log("right");
                    animator.SetFloat("angle", 7);
                }
            }

            if (angle > 112.5 && angle < 157.5)
            {
                if (crossProduct.y > 0)
                {
                    //Debug.Log("backward_left");
                    animator.SetFloat("angle", 4);
                }
                else if (crossProduct.y < 0)
                {
                    //Debug.Log("backward_right");
                    animator.SetFloat("angle", 6);
                }
            }

            if (angle > 157.5)
            {
                //Debug.Log("backward");
                animator.SetFloat("angle", 5);
            }
        }
        //else
        //{
        //    animator.SetFloat("angle", 0);
        //}
    }

    public void SetWeaponAnm(int value)
    {
        animator.SetFloat("weapon", value);
    }
    private void OnAnimatorMove()
    {
        if (playerLocomotion != null)
        {
            float delta = Time.deltaTime;
            // enemyManager.enemyRb.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotion.characterController.Move(velocity*Time.deltaTime);
        }
    
    }

    public void OnAnimationFinish()
    {
        
        playerLocomotion.transform.position = animator.deltaPosition;
    }
}
