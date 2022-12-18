using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInputSystem : MonoBehaviour, IControllerInput
{

    public float HorizontalInput
    {
        get { return Input.GetAxisRaw("Horizontal"); }
    }
    public bool Sprinting
    {
        get { return Input.GetKey(KeyCode.LeftShift); }
    }

    public bool AttackMellee
    {
        get { return Input.GetKeyDown(KeyCode.K); }
    }
    public bool AttackProjectile
    {
        get { return Input.GetKeyDown(KeyCode.J); }
    }

    public bool JumpDown
    {
        get { return Input.GetKeyDown(KeyCode.Space); }
    }
    public bool JumpHold
    {
        get { return Input.GetKey(KeyCode.Space); }
    }
}
