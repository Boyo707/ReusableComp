using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimParams : MonoBehaviour
{
    [SerializeField] private Animator _bodyAnimator;
    [SerializeField] private Animator _legsAnimator;

    private Animator anim;
    private Rigidbody2D _rB;
    private Jump _jump;
    private KeyboardInput _kB;
    private slopeMovement _slope;
    private Health _health;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _rB = GetComponent<Rigidbody2D>();
        _jump = GetComponent<Jump>();
        _kB = GetComponent<KeyboardInput>();
        _slope = GetComponent<slopeMovement>();
        _health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        bool _throw = Input.GetButtonDown("Projectile Attack");
        bool attack = Input.GetButtonDown("Mellee Attack");
        float verticalSpeed = _rB.velocity.y;

        float horizontalSpeed = Input.GetAxisRaw("Horizontal");
        bool moving;
        if (horizontalSpeed != 0)
            moving = true;
        else
            moving = false;

        bool sprinting = _kB.Sprinting;

        bool onSlope = _slope.onSlope;

        bool hurt = _health.knocked;
        



        bool onGround = _jump.onGround;

        _bodyAnimator.SetFloat("VerticalVelocity", verticalSpeed);
        _bodyAnimator.SetBool("Moving", moving);
        _bodyAnimator.SetBool("Sprinting", sprinting);
        _bodyAnimator.SetBool("OnGround", onGround);
        _bodyAnimator.SetBool("OnSlope", onSlope);
        _bodyAnimator.SetBool("Hurt", hurt);

        _legsAnimator.SetFloat("VerticalVelocity", verticalSpeed);
        _legsAnimator.SetBool("Moving", moving);
        _legsAnimator.SetBool("Sprinting", sprinting);
        _legsAnimator.SetBool("OnGround", onGround);
        _legsAnimator.SetBool("OnSlope", onSlope);
        _legsAnimator.SetBool("Hurt", hurt);


        //bool isGrounded
        anim.SetBool("Moving", moving);
        anim.SetFloat("verticalVelocity", verticalSpeed);
        anim.SetFloat("m", horizontalSpeed);
        anim.SetBool("onGround", onGround);
        anim.SetBool("Sprinting", _kB.Sprinting);
        anim.SetBool("onSlope", _slope.onSlope);
        anim.SetBool("Hurt", _health.knocked);
    }
}
