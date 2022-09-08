using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour, IJump
{
    private Rigidbody2D rigid;

    [SerializeField] private LayerMask _layermask = 320;
    [SerializeField] private bool _autoJump;
    [SerializeField] private float _jumpForce = 15;
    [SerializeField] private float _doubleJumpForce = 10;
    [SerializeField] private float fallMultiplier = 3;
    [SerializeField] private float lowJumpMultiplier = 5;
    [SerializeField] private float jumpButtonGrace = 0.1f;

    private float? lastGroundTime;
    private float? jumpButtonPressedTime;

    private bool _doubleJumped = false;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void JumpInput(bool isGrounded, bool jumpDown, bool jumpHold = false)
    {
        if (isGrounded)
        {
            lastGroundTime = Time.time;
            _doubleJumped = false;
        }


        if (jumpDown)
            jumpButtonPressedTime = Time.time;

        if (Time.time - lastGroundTime <= jumpButtonGrace)
        {
            if (Time.time - jumpButtonPressedTime <= jumpButtonGrace)
            {
                Debug.Log("Jump!");
                rigid.velocity = Vector2.up * _jumpForce;
                jumpButtonPressedTime = null;
                lastGroundTime = null;
            }
        }

        if (!_doubleJumped && !isGrounded && jumpDown)
        {
            rigid.velocity = Vector2.up * _doubleJumpForce;
            _doubleJumped = true;
        }


        if (rigid.velocity.y < 0)
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rigid.velocity.y > 0 && !jumpHold)
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        

    }

}
