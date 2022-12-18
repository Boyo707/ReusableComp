using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PS4ControllerInputSystem : MonoBehaviour, IControllerInput
{
    //TODO: Find a way how Hold, Tap works in the new input System

    PS4Controlls _PS4Controls;

    InputAction _horizontalMovementInput;
    InputAction _sprintingInput;
    InputAction _attackInput;
    InputAction _throwInput;
    InputAction _jumpTapInput;
    InputAction _jumpHoldInput;

    private bool _tapJump;
    private bool _holdJump;
    private bool _sprinting;
    private bool _attack;
    private bool _throw;

    private void Awake()
    {
        _PS4Controls = new PS4Controlls();
    }

    private void OnEnable()
    {
        //_controls.Enable();

        _horizontalMovementInput = _PS4Controls.Player.HorizontalMovement;
        _sprintingInput = _PS4Controls.Player.Sprinting;
        _jumpTapInput = _PS4Controls.Player.JumpTap;
        _jumpHoldInput = _PS4Controls.Player.JumpHold;
        _attackInput = _PS4Controls.Player.Attack;
        _throwInput = _PS4Controls.Player.Throw;
        _jumpTapInput.Enable();
        _jumpHoldInput.Enable();
        _attackInput.Enable();
        _throwInput.Enable();
        _horizontalMovementInput.Enable();
        _sprintingInput.Enable();
    }

    private void OnDisable()
    {
        _horizontalMovementInput.Disable();
        _sprintingInput.Disable();
        _jumpTapInput.Disable();
        _jumpHoldInput.Disable();
        _attackInput.Disable();
        _throwInput.Disable();
    }

    private void Start()
    {
        #region Jump Interaction Assignments

        _jumpTapInput.started += context => { if (context.interaction is TapInteraction) _tapJump = true; };

        _jumpHoldInput.started += context => { if (context.interaction is HoldInteraction) _holdJump = true; };

        _jumpTapInput.performed += context => { if (context.interaction is TapInteraction) _tapJump = false; };

        _jumpHoldInput.performed += context => { if (context.interaction is HoldInteraction) _holdJump = false; };

        _jumpTapInput.canceled += context => { if (context.interaction is TapInteraction) _tapJump = false; };

        _jumpHoldInput.canceled += context => { if (context.interaction is HoldInteraction) _holdJump = false; };
        #endregion

        #region Sprinting
        _sprintingInput.started += context => { if (context.interaction is HoldInteraction) _sprinting = true; };
        
        _sprintingInput.canceled += context => { if (context.interaction is HoldInteraction) _sprinting = false; };
        #endregion

        #region Attacks
        _attackInput.started += context => { if (context.interaction is TapInteraction) _attack = true; };
        _attackInput.performed += context => { if (context.interaction is TapInteraction) _attack = false; };

        _throwInput.started += context => { if (context.interaction is TapInteraction) _throw = true; };
        _throwInput.performed += context => { if (context.interaction is TapInteraction) _throw = false; };
        #endregion
    }

    public float HorizontalInput
    {
        get { return _horizontalMovementInput.ReadValue<float>(); }
    }
    public bool Sprinting
    {
        get { return _sprinting; }
    }

    public bool AttackMellee
    {
        get { return _attack; }
    }
    public bool AttackProjectile
    {
        get { return _throw; }
    }

    public bool JumpDown
    {
        get { return _tapJump; }
    }
    public bool JumpHold
    {
        get { return _holdJump; }
    }
}
