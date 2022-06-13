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
    [SerializeField] private Vector2 _direction;
    [SerializeField] private bool _onSlope;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float force;

    private void Awake()
    {
        Debug.Log("awaken");
    }

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
        moveDirection = new Vector2(inputDirection.x, 0);
        _rb.AddForce(Vector2.down * force);
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
