using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class NewPlayerController : MonoBehaviour, IEntityController
{
    [SerializeField] private PhysicsMaterial2D _noFrictionMaterial;
    [SerializeField] private PhysicsMaterial2D _FrictionMaterial;

    private MovementState _walkMovement;
    private IControllerInput _controllerInput;
    private JumpState _jump;
    private AttackMellee _attackMellee;
    private AttackProjectile _attackProjectile;
    private GroundDetection _gD;

    private Rigidbody2D _rb2D;

    private bool _knockedBack;
    private float _knockedDuration;



    // Start is called before the first frame update
    void Start()
    {
        
        _controllerInput = GetComponent<IControllerInput>();
        _gD = GetComponent<GroundDetection>();
        _walkMovement = GetComponent<MovementState>();
        _jump = GetComponent<JumpState>();
        _attackMellee = GetComponent<AttackMellee>();
        _attackProjectile = GetComponent<AttackProjectile>();

        _rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_controllerInput.HorizontalInput);
        //a way to disable it at knockback.
        if (_knockedDuration > 0)
        {
            _knockedDuration -= Time.deltaTime;
            _knockedBack = true;

            if (_gD.OnGround())
                _rb2D.sharedMaterial = _FrictionMaterial;
            else
                _rb2D.sharedMaterial = _noFrictionMaterial;
        }
        else
        {
            _rb2D.sharedMaterial = _noFrictionMaterial;
            _knockedBack = false;
        }

        if(!_knockedBack)
            EntityControlls();
    }

    private void EntityControlls()
    {
        _jump.JumpInput(_gD.OnGround(), _controllerInput.JumpDown, _controllerInput.JumpHold);
        _walkMovement.MoveInput(_controllerInput.HorizontalInput, _controllerInput.Sprinting);
        _attackMellee.Attack(_controllerInput.AttackMellee);
        _attackProjectile.Attack(_controllerInput.AttackProjectile);
    }

    private void UIInputs()
    {
        //maybe
    }

    public void DisableEntityControlls(float duration)
    {
        _knockedDuration = duration;
    }
}
