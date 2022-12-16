using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputSystem : MonoBehaviour
{
    public float HorizontalInput
    {
        get { return Input.GetAxisRaw("Horizontal"); }
    }

    public bool AttackMellee
    {
        get { return Input.GetKeyDown(KeyCode.K); }
    }
    public bool AttackProjectile
    {
        get { return Input.GetKeyDown(KeyCode.J); }
    }

    public bool Sprinting
    {
        get { return Input.GetKey(KeyCode.LeftShift); }
    }

    public bool JumpDown
    {
        get { return Input.GetKeyDown(KeyCode.Joystick1Button1); }
    }
    public bool JumpHold
    {
        get { return Input.GetKey("joystick button 1"); }
    }
}
