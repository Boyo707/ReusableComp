using Newtonsoft.Json.Serialization;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;


[RequireComponent(typeof(Rigidbody2D), typeof(GroundDetection))]
public class MovementState : MonoBehaviour
{
    [Header("Grounded")]
    [Space]
    [Header("Speed Values")]
    [SerializeField] private float _groundedAcceleration = 0; 
    [SerializeField] private float _groundedMaxSpeed = 5;

    [Header("Deceleration values")]
    [SerializeField] private float _groundedDecel;

    [Header("Air")]
    [Space]
    [Header("Speed Values")]
    [SerializeField] private float _airAcceleration = 0;
    [SerializeField] private float _airMaxSpeed = 5;

    [Header("Deceleration values")]
    [SerializeField] private float _airDecel;

    private bool _facingRight = true;

    private float _accel;
    private float _maxSpeed;
    private float _decel;

    

    private PlayerPhysics _playerPhysics;
    private Rigidbody2D _rB;

    Vector2 _moveInput;

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }

    private void Start()
    {
        _playerPhysics = GetComponent<PlayerPhysics>();
        _rB = _playerPhysics.Rigidbody;
    }

    private void Update()
    {
        if (_playerPhysics.IsGrounded)
        {
            _accel = _groundedAcceleration;
            _playerPhysics.SetCurrentMaxSpeed(_groundedMaxSpeed);
            _decel = _groundedDecel;
        }
        else
        {
            _accel = _airAcceleration;
            _playerPhysics.SetCurrentMaxSpeed(_airMaxSpeed);
            _decel = _airDecel;
        }
    }

    private void FixedUpdate()
    {

        if (_moveInput.x > 0 && !_facingRight)
        {
            Flip();
        }
        else if (_moveInput.x < 0 && _facingRight)
        {
            Flip();
        }

        //acceleration
        if (_moveInput.x != 0)
        {
            _rB.AddForce(_accel * _moveInput.x * Vector2.right, ForceMode2D.Force);
        }
        //deceleration
        else if (_moveInput.x == 0)
        {
            if(_rB.velocity.x < 1 && _rB.velocity.x > -1)
            {
                _rB.velocity = new Vector2(0, _rB.velocity.y);
            }
            else
            {
                _rB.AddForce(_decel * -Mathf.Sign(_rB.velocity.x) * Vector2.right, ForceMode2D.Force);

            }
        }
        //_rB.velocity = new Vector2(Mathf.Clamp(_rB.velocity.x, -_maxSpeed, +_maxSpeed), _rB.velocity.y);
    }

    private void Flip()
    {
        Vector3 currrentScale = gameObject.transform.localScale;
        currrentScale.x *= -1;
        gameObject.transform.localScale = currrentScale;

        _facingRight = !_facingRight;
    }

    public bool FacingRight()
    {
        return _facingRight;
    }
}
