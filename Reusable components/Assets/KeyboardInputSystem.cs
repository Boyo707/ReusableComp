using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputSystem : MonoBehaviour
{
    public float horizontalInput
    {
        get { return Input.GetAxisRaw("Horizontal"); }
    }

    public bool sprinting
    {
        get { return Input.GetKey(KeyCode.LeftShift); }
    }

    public bool jumpDown
    {
        get { return Input.GetKeyDown(KeyCode.Space); }
    }
    public bool jumpHold
    {
        get { return Input.GetKey(KeyCode.Space); }
    }
}
