using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private KeyboardInput _kB;
    private IJump _jump;
    private IMovements _movement;
    // Start is called before the first frame update
    void Start()
    {
        _kB = GetComponent<KeyboardInput>();
        _jump = GetComponent<IJump>();
        _movement = GetComponent<IMovements>();
    }

    // Update is called once per frame
    private void Update()
    {
        _jump.GroundCheck();
        _jump.JumpInput(_kB.jumpDown,  _kB.jumpHold);
        
    }

    private void FixedUpdate()
    {
        _movement.MoveInput(_kB.moveInput, _kB.Sprinting);
        

    }
}
