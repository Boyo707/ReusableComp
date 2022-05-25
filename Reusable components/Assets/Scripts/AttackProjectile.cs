using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour, IAttack
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float _startupLag;
    [SerializeField] private float _endLag;

    private float timer;

    public void Attack(bool attack, bool facingLeft)
    {
        //shoot + delay count down

        projectile.GetComponent<Projectile>().FacingLeft(facingLeft);
        if (timer <= 0)
        {
            if (attack)
            {
                
                Instantiate(projectile, gameObject.transform);
                timer += _endLag;
            }
        }
        else if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
            
        
    }
}
