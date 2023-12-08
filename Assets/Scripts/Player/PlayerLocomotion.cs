using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerLocomotion : MonoBehaviour
{
    public PlayerInventory playerInventory;
    private PlayerAnimationCtrl playerAnimationCtrl;
    private WeaponSlotManager weaponSlotManager;
    public CharacterController characterController;

    public Transform groundCheck;

    public float speed;
    public float gravity = -9.81f;
    public float rotationAngleY;

    public float groundDistance = 0.01f;

    private Vector3 direction;
    public Vector3 velocity;

    public bool isGrounded;
    public bool isFalling;
    public bool isInteracting;

    public float turnSmoothTime = 0.05f;
    private float turnSmoothVelocity;

    public LayerMask groundMask;

    private void Start()
    {
        playerAnimationCtrl = GetComponentInChildren<PlayerAnimationCtrl>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        DayNightCycle.OnDay += kk;
        DayNightCycle.OnNight += hh;
    }

    public void kk() { Debug.Log("day"); }
    public void hh() { Debug.Log("night"); }

    private void Update()
    {
        if (playerAnimationCtrl.animator.GetBool("isInteracting"))
            return;
        HandleRolling();
    }

    private void FixedUpdate()
    {
        if (playerAnimationCtrl.animator.GetBool("isInteracting"))
            return; 
        HandleRotate();
        HandleMovement();
        HandleFallingAndLanding();    
    }

    private void HandleRotate()
    {
        direction = new Vector3(InputHandle.Instance.horizontal, 0, InputHandle.Instance.vertical);

        if (direction.magnitude > 0.1)
        {
            if(weaponSlotManager.weapon==null || !InputHandle.Instance.hold)
            {
                playerAnimationCtrl.animator.SetBool("isHolding", false);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }

    private void HandleMovement()
    {
        direction = new Vector3(InputHandle.Instance.horizontal, 0, InputHandle.Instance.vertical);

        if (direction.magnitude > 0.1)
        {
            rotationAngleY = Camera.main.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, rotationAngleY, 0);
            direction = rotation * direction;

            float angle = Vector3.Angle(transform.forward, direction);
            Vector3 crossProduct = Vector3.Cross(direction.normalized, transform.forward);

            playerAnimationCtrl.UpdateValueAnimation(direction.magnitude, angle, crossProduct);

            if (InputHandle.Instance.hold && weaponSlotManager.weapon != null)
            {
                characterController.Move(direction.normalized * speed * 0.75f * Time.fixedDeltaTime);
            }
            else
            {
                characterController.Move(direction.normalized * speed * 1.5f * Time.fixedDeltaTime);
            }
        }
        else
        {
            playerAnimationCtrl.UpdateValueAnimation(0, 0, Vector3.zero);
        }
    }

    private void HandleFallingAndLanding()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (!isGrounded)
        {
            if (!isInteracting)
            {
                isFalling = true;
                //playerAnimator.PlayerTargetAnimation("player_falling", true);
            }

            velocity.y += gravity / 2 * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);
        }

        if (isGrounded && isFalling && isInteracting)
        {
            //playerAnimator.PlayerTargetAnimation("player_landing", true);
            isFalling = false;
        }
    }

    private void HandleRolling() 
    { 
    
        if(InputHandle.Instance.roll && !InputHandle.Instance.hold)
        {
            if(playerInventory.playerStats.useStamina(25))
                playerAnimationCtrl.PlayerTargetAnimation("player_rolling", true);
        }        
    }
}
