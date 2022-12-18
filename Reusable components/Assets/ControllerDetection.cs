using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class ControllerDetection : MonoBehaviour
{
    PS4Controlls _ps4controlls;

    InputAction _detection;


    private bool _PS4Detected = false;


    private void Awake()
    {
        _ps4controlls = new PS4Controlls();
        _detection = _ps4controlls.ControllerDetection.Detection;
    }

    private void OnEnable()
    {
        _detection.Enable();
    }

    private void OnDisable()
    {
        _detection.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        _detection.started += context => { if (context.interaction is TapInteraction) _PS4Detected = true; };
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<KeyboardInputSystem>())
        {
            if (_PS4Detected = true && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space)))
            {
                GetComponent<PS4ControllerInputSystem>().enabled = false;

                GetComponent<KeyboardInputSystem>().enabled = true;

                _PS4Detected = false;
            }
        }
        if (!GetComponent<PS4ControllerInputSystem>()) 
        {
            if (_PS4Detected)
            {
                GetComponent<KeyboardInputSystem>().enabled = false;

                GetComponent<PS4ControllerInputSystem>().enabled = true;
            }
        }

        
    }

}
