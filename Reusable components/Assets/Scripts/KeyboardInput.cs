using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private Vector2 _moveInput;
    // Start is called before the first frame update
    void Start()
    {
        //Ik wil dat ik de movement er in kan gooien zonder dat ik een ander script nodig heb.
        //PlayerController kan dan de keyboard input gebruiken en op de jump en movement gooien
        //maar ik wil zo min mogelijk hard coden. maar dat zal hoe dan ook nodig zijn.
    }
    public bool Sprinting
    {
        get { return Input.GetKey(KeyCode.LeftShift); }
    }

    public bool jumpHold
    {
        get { return Input.GetButton("Jump"); }
    }
    public bool jumpDown
    {
        get { return Input.GetButtonDown("Jump"); }
    }

    public bool projectileAttack
    {
        get { return Input.GetButtonDown("Projectile Attack"); }
    }

    public bool melleeAttack
    {
        get { return Input.GetButtonDown("Mellee Attack"); }
    }

    public Vector2 moveInput
    {
        get { return _moveInput; }
    }


    // Update is called once per frame
    void Update()
    {
        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
    }
}
