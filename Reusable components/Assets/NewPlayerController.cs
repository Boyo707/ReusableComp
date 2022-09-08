using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private HorizontalMovement _walkMovement;
    private KeyboardInputSystem _kBI;
    private Jump _jump;
    private GroundDetection _gD;


    // Start is called before the first frame update
    void Start()
    {
        _walkMovement = GetComponent<HorizontalMovement>();
        _kBI = GetComponent<KeyboardInputSystem>();
        _jump = GetComponent<Jump>();
        _gD = GetComponent<GroundDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        _jump.JumpInput(_gD.OnGround(), _kBI.jumpDown, _kBI.jumpHold);
        _walkMovement.MoveInput(_kBI.horizontalInput, _kBI.sprinting);
    }

}
