using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour 
{
    [Header("Health")]
    [SerializeField] private bool _invincible = false;
    [Space]
    [SerializeField] private float _health = 10;
    [SerializeField] private int _lives = 3;

    [Header("Death")]
    [Space]
    [SerializeField] private UnityEvent _onDeath;
    
    //public event onDeath OnDeath = delegate { };

    //delegate/event maken voor onDeath. Add particle system/active particle, death animation, destory en of voor de player een game over screen met een respawn/retry button?

    public float HealthInt { get { return _health; } set { _health = value; } }

    public int LivesInt { get { return _lives; } set { _lives = value; } }

    public void TakeDamage(float damage)
    {
        if (!_invincible)
        {
            _health -= damage;
        }
    }

    public void ApplyKnockBack(float angle, float knockbackForce, float duration)
    {
        if (!_invincible)
        {

            //GetComponent<IEntityController>().DisableEntityControlls(duration);
            //SwitchPlayerState(PlayerStates.KnockedBack);
            var Angle = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * knockbackForce,
            Mathf.Sin(angle * Mathf.Deg2Rad) * knockbackForce);
            GetComponent<Rigidbody2D>().velocity = Angle;
        }
    }
}
