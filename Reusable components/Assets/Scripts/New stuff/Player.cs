using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using TMPro.EditorUtilities;

public enum PlayerStates
{
    Default,
    KnockedBack,
    Dashing,
    Death,
    Frozen
}

[System.Serializable]
public class Player : MonoBehaviour
{

    [SerializeField] private Behaviour[] _MovementComponents;
    [SerializeField] private Behaviour[] _AttackComponents;

    private Rigidbody2D _rB;

    private GroundDetection _gD;

    private float _knockedDuration;

    private PlayerStates _currentState;

    private bool _canAttack = true;
    private bool _canMove = true;

    public PlayerStates CurrentState { get { return _currentState; } }

    // Start is called before the first frame update
    void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
        _gD = GetComponent<GroundDetection>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_canAttack)
        {
            foreach (var component in _AttackComponents)
            {
                component.enabled = false;
            }
        }
        else
        {
            foreach (var component in _AttackComponents)
            {
                component.enabled = true;
            }
        }

        if (!_canMove)
        {
            foreach (var component in _MovementComponents)
            {
                component.enabled = false;
            }
            _rB.velocity = Vector2.zero;
        }
        else
        {
            foreach (var component in _MovementComponents)
            {
                component.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _canMove = !_canMove;
        }
    }

}
