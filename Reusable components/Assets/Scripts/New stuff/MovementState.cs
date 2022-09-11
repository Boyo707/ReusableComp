using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.XR;
using static UnityEngine.UI.Button;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementState : MonoBehaviour, IMovements
{
    [Header("Required Components")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spR;
    
    [Header("Speed Values")]
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _sprintSpeed;

    [Header("Smoothness")]
    [SerializeField] private float _smoothInputSpeed = 0.083f;
    [SerializeField] private float _smoothSprintTransition = 0.5f;


    private float moveDirection;
    private float _currentSpeed;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;

    private float t = 0.0f;

    /// <summary>
    /// this commented code is used to make a function in the editor that acts like the OnClick fucntion on the UI button.
    /// </summary>
    /*
    [Serializable] public class PlayerState : UnityEvent { }

    [FormerlySerializedAs("gangamStyle")]
    [SerializeField] private PlayerState Yes = new PlayerState();

    public PlayerState poop
    {
        get { return Yes; }
        set { Yes = value; }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            Yes.Invoke();
    }

    public void fart()
    {
        Debug.Log("Pffffttt");
    }*/


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



        if (_rb.velocity.x > 0)
        {
            _spR.flipX = false;
        }
        else if(_rb.velocity.x < 0)
        {
            _spR.flipX = true;
        }
    }

}
