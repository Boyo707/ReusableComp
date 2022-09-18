using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour, IHealth 
{
    [SerializeField] private bool _invincible = false;

    [SerializeField] private float _health = 10;
    [SerializeField] private int _lives = 3;

    
    //public event onDeath OnDeath = delegate { };

    //delegate/event maken voor onDeath. Add particle system/active particle, death animation, destory en of voor de player een game over screen met een respawn/retry button?

    public float HealthInt { get { return _health; } set { _health = value; } }

    public int LivesInt { get { return _lives; } set { _lives = value; } }

    public void TakeDamage(float damage)
    {
        if(!_invincible)
            _health -= damage;
    }
}
