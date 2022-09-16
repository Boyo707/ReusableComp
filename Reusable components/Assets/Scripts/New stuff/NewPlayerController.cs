using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private MovementState _walkMovement;
    private KeyboardInputSystem _kBI;
    private JumpState _jump;
    private GroundDetection _gD;
    private IdleState _idle;
    private StateBase _stateBase;


    private Rigidbody2D _rb;

    [SerializeField] private float _damageAmount;
    [SerializeField] private LayerMask _damagableLayers;

    [SerializeField] private float _knockBackForce;


    // Start is called before the first frame update
    void Start()
    {
        _walkMovement = GetComponent<MovementState>();
        _kBI = GetComponent<KeyboardInputSystem>();
        _jump = GetComponent<JumpState>();
        _gD = GetComponent<GroundDetection>();
        _idle = GetComponent<IdleState>();
        _stateBase = GetComponent<StateBase>();

        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _jump.JumpInput(_gD.OnGround(), _kBI.JumpDown, _kBI.JumpHold);
        _walkMovement.MoveInput(_kBI.HorizontalInput, _kBI.Sprinting);
    }

}
