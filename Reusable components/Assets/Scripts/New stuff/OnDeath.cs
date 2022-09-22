using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Health))]
public class OnDeath : MonoBehaviour
{
    [Serializable] public class OnDeathEvent : UnityEvent { }

    [SerializeField] private OnDeathEvent _onDeath = new OnDeathEvent();

    [SerializeField] private KeyCode _killButton;

    private Health _health;

    public OnDeathEvent pop
    {
        get { return _onDeath; }
        set { _onDeath = value; }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_killButton))
            _onDeath.Invoke();
        if (_health.LivesInt <= 0)
            _onDeath.Invoke();
    }

}
