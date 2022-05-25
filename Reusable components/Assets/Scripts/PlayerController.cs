using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private KeyboardInput _kB;
    private SpriteRenderer _spR;
    private IJump _jump;
    private IMovements _movement;
    private IAttack _attack;
    // Start is called before the first frame update
    void Start()
    {
        _kB = GetComponent<KeyboardInput>();
        _spR = GetComponent<SpriteRenderer>();
        _jump = GetComponent<IJump>();
        _movement = GetComponent<IMovements>();
        _attack = GetComponent<IAttack>();
    }

    // Update is called once per frame
    private void Update()
    {
        _jump.GroundCheck();
        _jump.JumpInput(_kB.jumpDown, _kB.jumpHold);
        _attack.Attack(_kB.projectileAttack, _spR.flipX) ;
        Debug.Log(_spR.flipX + " SPRITEFLIP");
    }

    private void FixedUpdate()
    {
        _movement.MoveInput(_kB.moveInput, _kB.Sprinting);
        

    }
}
