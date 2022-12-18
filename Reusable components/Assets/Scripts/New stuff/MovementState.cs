using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.UI.Button;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementState : MonoBehaviour, IMovements
{
    [Header("Required Components")]
    [SerializeField] private Rigidbody2D _rb;
    
    [Header("Speed Values")]
    [SerializeField] private float _defaultSpeed = 5;
    [SerializeField] private float _sprintSpeed = 8;

    [Header("Smoothness")]
    [SerializeField] private float _smoothInputSpeed = 0.083f;
    [SerializeField] private float _smoothSprintTransition = 0.5f;


    private bool _facingRight = true;


    private float moveDirection;
    private float _currentSpeed;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;

    private float t = 0.0f;

    public void MoveInput(float inputDirection, bool sprinting = false)
    {
        if (sprinting == true)
        {
            _currentSpeed = Mathf.Lerp(_defaultSpeed, _sprintSpeed + _defaultSpeed, t);
            t += _smoothSprintTransition * Time.deltaTime;
        }
        else
        {
            t = 0;
            _currentSpeed = _defaultSpeed;
        }

        
        currentInputVector = Vector2.SmoothDamp(currentInputVector, new Vector2(inputDirection, _rb.velocity.y), ref smoothInputVelocity, _smoothInputSpeed);
        moveDirection = currentInputVector.x;
        
        _rb.velocity = new Vector2(moveDirection * _currentSpeed, _rb.velocity.y); //moet gezet worden in fixed updates.
        


        if (inputDirection > 0 && !_facingRight)
        {
            Flip();
        }
        else if(inputDirection < 0 && _facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 currrentScale = gameObject.transform.localScale;
        currrentScale.x *= -1;
        gameObject.transform.localScale = currrentScale;

        _facingRight = !_facingRight;
    }

}
