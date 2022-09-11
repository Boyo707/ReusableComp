using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.XR;
using static UnityEngine.UI.Button;

public class MovementState : MonoBehaviour, IMovements
{
    private SpriteRenderer spriteRenderer;
    private float moveDirection;
    [SerializeField] private float _defaultSpeed;
    private float _currentSpeed;
    [SerializeField] private float _sprintSpeed;

    [SerializeField] private float _smoothInputSpeed = 0.083f;

    [SerializeField] private float _smoothSprintTransition = 0.5f;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;

    private float t = 0.0f;

    /// <summary>
    /// this commented code is used to make a function in the editor that acts like the OnClick fucntion on the UI button.
    /// </summary>
    /*
    [Serializable] public class PlayerState : UnityEvent { }

    [FormerlySerializedAs("gangamStyle")]
    [SerializeField] private PlayerState Yes = new PlayerState();

    public PlayerState poop
    {
        get { return Yes; }
        set { Yes = value; }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            Yes.Invoke();
    }

    public void fart()
    {
        Debug.Log("Pffffttt");
    }*/

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void MoveInput(Rigidbody2D rb, float inputDirection, bool sprinting = false)
    {
        if (sprinting == true)
        {
            _currentSpeed = Mathf.Lerp(_defaultSpeed, _sprintSpeed + _defaultSpeed, t);
            t += _smoothSprintTransition * Time.deltaTime;
        }
        else
        {
            t = 0;
            _currentSpeed = _defaultSpeed;
        }

        currentInputVector = Vector2.SmoothDamp(currentInputVector, new Vector2(inputDirection, rb.velocity.y), ref smoothInputVelocity, _smoothInputSpeed);
        moveDirection = currentInputVector.x;

        rb.velocity = new Vector2(moveDirection * _currentSpeed, rb.velocity.y); //moet gezet worden in fixed updates.



        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

}
