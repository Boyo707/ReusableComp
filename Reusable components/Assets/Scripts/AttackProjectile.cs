using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private bool infinite;
    [SerializeField] private int projectileAmount;
    [SerializeField] private float _startupLag;
    [SerializeField] private float _endLag;

    [SerializeField]private Animator _bodyAnim;

    private float timer;
    private Vector2 origin;
    private Vector3 gay;

    public int projectileAmounts { get { return projectileAmount; } set { projectileAmount = value; } }

    public void Attack(bool attack, bool facingLeft)
    {
        //shoot + delay count down
        if (facingLeft)
            origin = new Vector2(gameObject.transform.position.x - gameObject.transform.localScale.x / 1.25f , gameObject.transform.position.y);
        else
            origin = new Vector2(gameObject.transform.position.x - gameObject.transform.localScale.x / -1.20f, gameObject.transform.position.y);


        projectile.GetComponent<ITrajectory>().facingLeft(facingLeft);

        if (timer <= 0)
        {

            if (attack && projectileAmount != 0 || attack && infinite)
            {
                if(GetComponent<IPlayer>() != null)
                    _bodyAnim.SetBool("Throw", true);
                
                Instantiate(projectile, origin, Quaternion.identity, gameObject.transform);
                projectileAmount -= 1;
                timer += _endLag;
            }
        }
        else if(timer > 0)
        {
            timer -= Time.deltaTime;
            if (GetComponent<IPlayer>() != null)
                _bodyAnim.SetBool("Throw", false);
        }
            
        
    }


}
