using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Movement,
    Jumping,
    Idle
}

public class StateBase : MonoBehaviour
{

    /*private State _currentState;
    protected KeyboardInputSystem _kBI;
    protected Rigidbody2D _rb;
    protected SpriteRenderer _spR;


    void Start()
    {
        _kBI = GetComponent<KeyboardInputSystem>();
        _rb = GetComponent<Rigidbody2D>();
        _spR = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void EnterState()
    {

    }
    
    public virtual void StateUpdate()
    {

    }

    public virtual void ExitState()
    {

    }

    public State GetCurrentState()
    {
        return _currentState;
    }

    public void ChangeStateTo(State nextState)
    {
        ExitState();
        _currentState = nextState;
        EnterState();
    }*/
}
