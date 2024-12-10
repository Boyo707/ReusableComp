using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInputSystem : MonoBehaviour, IControllerInput
{
    private bool _stopInput = false;
    public float HorizontalInput
    {
        get 
        {
            if (!_stopInput)
            {
                return Input.GetAxisRaw("Horizontal");
            }
            return 0;
        }
    }
    public bool DashInput
    {
        get 
        {
            if (!_stopInput)
            {
                return Input.GetKeyDown(KeyCode.LeftShift);
            }
            return false;
        }
    }

    public bool AttackMellee
    {
        get 
        {
            if (!_stopInput)
            {
                return Input.GetKeyDown(KeyCode.K);
            }
            return false;
        }
    }
    public bool AttackProjectile
    {
        get 
        {
            if (!_stopInput)
            {
                return Input.GetKeyDown(KeyCode.J);
            }
            return false;
        }
    }

    public bool JumpDown
    {
        get 
        {
            if (!_stopInput)
            {
                return Input.GetKeyDown(KeyCode.Space);
            }
            return false;
        }
    }
    public bool JumpHold
    {
        get 
        {
            if (!_stopInput)
            {
                return Input.GetKey(KeyCode.Space);
            }
            return false;
        }
    }


    public void StopInput(bool isStopping)
    {
        _stopInput = isStopping;
    }
}
