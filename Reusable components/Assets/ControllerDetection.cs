using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDetection : MonoBehaviour
{
    bool isControllerConnected = false;
    public string Controller = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (Input.GetJoystickNames()[0] != null)
            {
                isControllerConnected = true;
                IdentifyController();
            }
        }
        catch
        {
            isControllerConnected = false;
        }
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                print("joystick 1 button " + i);
            }
        }

        
    }

    void IdentifyController()
    {
        Controller = Input.GetJoystickNames()[0];
    }
    }
