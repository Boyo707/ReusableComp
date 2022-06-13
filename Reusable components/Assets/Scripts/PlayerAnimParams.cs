using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimParams : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D _rB;
    private Jump _jump;
    private KeyboardInput _kB;
    private slopeMovement _slope;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _rB = GetComponent<Rigidbody2D>();
        _jump = GetComponent<Jump>();
        _kB = GetComponent<KeyboardInput>();
        _slope = GetComponent<slopeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalSpeed = _rB.velocity.y;

        float horizontalSpeed = Input.GetAxisRaw("Horizontal");
        bool moving;
        if (horizontalSpeed != 0)
            moving = true;
        else
            moving = false;

        bool onGround = _jump.onGround;

        //bool isGrounded
        anim.SetBool("Moving", moving);
        anim.SetFloat("verticalVelocity", verticalSpeed);
        anim.SetFloat("m", horizontalSpeed);
        anim.SetBool("onGround", onGround);
        anim.SetBool("Sprinting", _kB.Sprinting);
        anim.SetBool("onSlope", _slope.onSlope);
    }
}
