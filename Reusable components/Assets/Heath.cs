using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heath : MonoBehaviour, IHealth 
{
    [SerializeField] private int _health;

    private ParticleSystem _particles;

    //delegate/event maken voor onDeath. Add particle system/active particle, death animation, destory en of voor de player een game over screen met een respawn/retry button?

    // Start is called before the first frame update
    void Start()
    {
        _particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
            OnDeath();
    }

    //UI maken voor de player waar je de health kan zien.

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _particles.Play();
        Debug.Log(_health);
    }

    public void OnDeath()
    {
        //enemy/player animparams krijgen de parameter van de death animation. //Start particle effect
        _particles.Play();
        Debug.Log("I AM DEAD");
        Destroy(gameObject);
    }
}
