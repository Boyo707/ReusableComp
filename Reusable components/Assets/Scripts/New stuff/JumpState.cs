using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpState : MonoBehaviour, IJump
{

    [Header("Required Components")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 15;
    [SerializeField] private float fallMultiplier = 3;
    [SerializeField] private float lowJumpMultiplier = 5;

    [Header("Coyote Jump")]
    [SerializeField] private float jumpButtonGrace = 0.1f;

    [Header("Misc")]
    [SerializeField] private bool _autoJump;
    [SerializeField] private float _doubleJumpAmount;


    private float? lastGroundTime;
    private float? jumpButtonPressedTime;

    

    private float temp;

    private void Start()
    {
        temp = _doubleJumpAmount;
    }



    public void JumpInput(bool isGrounded, bool jumpDown, bool jumpHold = false)
    {

        if (isGrounded)
        {
            _doubleJumpAmount = temp;
            lastGroundTime = Time.time;
        }

        if (jumpDown)
            jumpButtonPressedTime = Time.time;

        if (Time.time - lastGroundTime <= jumpButtonGrace)
        {
            if (Time.time - jumpButtonPressedTime <= jumpButtonGrace)
            {
                _rb.velocity = Vector2.up * _jumpForce;
                jumpButtonPressedTime = null;
                lastGroundTime = null;
            }
        }

        if (_doubleJumpAmount != 0)
            if (!isGrounded)
                if (jumpDown)
                {
                    Debug.Log("2");
                    jumpButtonPressedTime = null;
                    lastGroundTime = null;
                    _rb.velocity = Vector2.up * _jumpForce / 1.25f;
                    _doubleJumpAmount -= 1;
                }

        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !jumpHold)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }

}
