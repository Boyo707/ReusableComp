using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayer
{
    private KeyboardInput _kB;
    private SpriteRenderer _spR;
    private IJump _jump;
    private IMovements[] _movement;
    private IAttack _attack;
    private IHealth _health;
    private slopeMovement _slope;
    private Rigidbody2D _rb;

    [SerializeField]private int movementType;
    private float time;
    private float _defaultGravity;
    // Start is called before the first frame update
    void Start()
    {
        _kB = GetComponent<KeyboardInput>();
        _spR = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _jump = GetComponent<IJump>();
        _movement = GetComponents<IMovements>();
        _attack = GetComponent<IAttack>();
        _health = GetComponent<IHealth>();
        _slope = GetComponent<slopeMovement>();
        //Debug.Log(_movement[0] + " " + _movement[1]);
        _defaultGravity = _rb.gravityScale;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_health.knocked)
        {
            _jump.GroundCheck();
            _jump.JumpInput(_kB.jumpDown, _kB.jumpHold);
            _attack.Attack(_kB.projectileAttack, _spR.flipX);
        }
        movementSwitcher();
    }


    private void FixedUpdate()
    {
        if (!_health.knocked)
        {
            _movement[movementType].MoveInput(_kB.moveInput, _kB.Sprinting);
        }
    }

    private void movementSwitcher()
    {
        if (movementType == 1 && _kB.moveInput == Vector2.zero)
            _rb.gravityScale = 0;
        else
            _rb.gravityScale = _defaultGravity;

        if (_kB.jumpDown)
        {
            movementType = 0;
            time = 0.1f;
        }

        if (_slope.onSlope == false && _jump.onGround)
            movementType = 0;
        /*else if (!_jump.onGround)
        {
            movementType = 0;

        }*/
        else if (_slope.onSlope && time <= 0)
            movementType = 1;


        if (time > 0)
        {
            time -= Time.deltaTime;

        }
    }
}
