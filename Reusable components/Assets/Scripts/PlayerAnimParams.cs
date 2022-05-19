using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimParams : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D _rB;
    private Jump _jump;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _rB = GetComponent<Rigidbody2D>();
        _jump = GetComponent<Jump>();
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

        if (verticalSpeed <= -2 && onGround)
            Debug.Log("OVER -2 I AM = " + verticalSpeed);

            //bool isGrounded
            anim.SetBool("Moving", moving);
        anim.SetFloat("verticalVelocity", verticalSpeed);
        anim.SetFloat("m", horizontalSpeed);
        anim.SetBool("onGround", onGround);
    }
}
