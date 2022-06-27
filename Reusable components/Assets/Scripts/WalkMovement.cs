using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMovement : MonoBehaviour, IMovements
{
    private Rigidbody2D _rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;
    [SerializeField] private float _defaultSpeed;
    private float _currentSpeed;
    [SerializeField] private float _sprintSpeed;

    [SerializeField] private float smoothInputSpeed = 0.2f;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _currentSpeed = _defaultSpeed;
    }

    private void Update()
    {

    }


    private void NormalMovement(Vector2 inputDirection, bool sprinting)
    {
        currentInputVector = Vector2.SmoothDamp(currentInputVector, inputDirection, ref smoothInputVelocity, smoothInputSpeed);
        moveDirection = new Vector2(currentInputVector.x, 0);
        _rb.velocity = new Vector2(moveDirection.x * _currentSpeed, _rb.velocity.y); //moet gezet worden in fixed updates.

        if (sprinting == true)
            _currentSpeed = _sprintSpeed + _defaultSpeed;
        else
            _currentSpeed = _defaultSpeed;

        

    }


    public void MoveInput(Vector2 inputDirection, bool sprinting = false)
    {
        NormalMovement(inputDirection, sprinting);

        if (_rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(_rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }



}
