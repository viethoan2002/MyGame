using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandle : MonoBehaviour
{
    public static InputHandle Instance;
    private PlayerControls playerControls;

    public Vector2 movementInput;
   // public Vector3 mousePosition;

    public float horizontal;
    public float vertical;

    public Vector2 zoom;

    public bool jump;
    public bool re_attack;
    public bool use;
    public bool hold;
    public bool run;
    public bool attack;
    public bool roll;

    private void Awake()
    {
        if (InputHandle.Instance == null)
        {
            Instance = this;
            playerControls = new PlayerControls();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        MoveInput();
        ZoomInput();
        UseInput();
        HoldInput();
        AttackInput();
        RunInput();
        RollInput();
    }

    private void RunInput()
    {
        playerControls.PlayerAction.Run.started += inputAction => run = true;
        playerControls.PlayerAction.Run.canceled += inputAction => run = false;
    }

    private void HoldInput()
    {
        playerControls.PlayerAction.Hold.started += inputAction => hold = true;
        playerControls.PlayerAction.Hold.canceled += inputAction => hold = false;
    }

    private void UseInput()
    {
        playerControls.PlayerAction.Use.performed += inputAction => use = true;
    }

    private void ZoomInput()
    {
        playerControls.Camera.Zoom.performed += inputAction => zoom = inputAction.ReadValue<Vector2>();
    }

    private void MoveInput()
    {
        playerControls.PlayerMovement.Movement.performed += inputAction => movementInput = inputAction.ReadValue<Vector2>();
        horizontal = movementInput.x;
        vertical = movementInput.y;
    }

    private void AttackInput()
    {
        if (hold)
        {
            playerControls.PlayerAction.Attack.started += inputAction => attack = true;
            playerControls.PlayerAction.Attack.canceled += inputAction => attack = false;
        }
        else
        {
            attack = false;
        }
    }

    private void RollInput()
    {
        playerControls.PlayerAction.Rolling.performed += inputAction => roll = true;
    }

    private void LateUpdate()
    {
        InputHandle.Instance.use = false;
        InputHandle.Instance.roll = false;
    }
}
