using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashDuration;

    private bool _isDashing;
    private float _tempTime;

    private PlayerPhysics _physics;
    private Rigidbody2D _rB;

    private JumpState _jumpState;

    private void Start()
    {
        _physics = GetComponent<PlayerPhysics>();
        _rB = _physics.Rigidbody;

        _jumpState = GetComponent<JumpState>();

        _tempTime = _dashDuration;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _physics.SetChangingSpeed(_dashForce);
            if (_physics.IsFacingRight)
            {
                _rB.velocity = Vector2.zero;
                _rB.AddForce(Vector2.right * _dashForce, ForceMode2D.Impulse);
            }
            else
            {
                _rB.velocity = Vector2.zero;
                _rB.AddForce(Vector2.left * _dashForce, ForceMode2D.Impulse);
            }
            _isDashing = true;
        }

        if (_isDashing)
        {
            if (_tempTime > 0)
            {
                _tempTime -= Time.deltaTime;
                _rB.velocity = new Vector2(_rB.velocity.x, 0);
            }
            else
            {
                _isDashing = false;
                _tempTime = _dashDuration;
            }
        }

    }
}
