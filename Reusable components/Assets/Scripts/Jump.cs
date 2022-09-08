using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Jump : MonoBehaviour, IJump
{

    private Rigidbody2D rigid;


    [SerializeField] private bool _autoJump;
    [SerializeField] private float _jumpForce = 15;

    [Header("Fall when let go of button")]
    [SerializeField] private float fallMultiplier = 3;
    [SerializeField] private float lowJumpMultiplier = 5;
    [Header("Coyote Jump")]
    [SerializeField] private float jumpButtonGrace = 0.1f;

    private float? lastGroundTime;
    private float? jumpButtonPressedTime;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }



    public void JumpInput(bool isGrounded, bool jumpDown, bool jumpHold = false)
    {

        if (isGrounded)
            lastGroundTime = Time.time;

        if (jumpDown)
            jumpButtonPressedTime = Time.time;

        if (Time.time - lastGroundTime <= jumpButtonGrace)
        {
            if (Time.time - jumpButtonPressedTime <= jumpButtonGrace)
            {
                rigid.velocity = Vector2.up * _jumpForce;
                jumpButtonPressedTime = null;
                lastGroundTime = null;
            }
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
