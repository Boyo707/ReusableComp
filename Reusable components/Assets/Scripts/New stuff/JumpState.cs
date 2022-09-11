using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class JumpState : MonoBehaviour, IJump
{

    [SerializeField] private bool _autoJump;
    [SerializeField] private float _jumpForce = 15;

    [Header("Fall when let go of button")]
    [SerializeField] private float fallMultiplier = 3;
    [SerializeField] private float lowJumpMultiplier = 5;
    [Header("Coyote Jump")]
    [SerializeField] private float jumpButtonGrace = 0.1f;

    private float? lastGroundTime;
    private float? jumpButtonPressedTime;

    [SerializeField]private float _doubleJumpAmount;

    private float temp;

    private void Start()
    {
        temp = _doubleJumpAmount;
    }



    public void JumpInput(Rigidbody2D rb, bool isGrounded, bool jumpDown, bool jumpHold = false)
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
                rb.velocity = Vector2.up * _jumpForce;
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
                    rb.velocity = Vector2.up * _jumpForce / 1.25f;
                    _doubleJumpAmount -= 1;
                }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !jumpHold)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }

}
