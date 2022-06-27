using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour, IAttack
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private bool infinite;
    [SerializeField] private int projectileAmount;
    [SerializeField] private float _startupLag;
    [SerializeField] private float _endLag;

    private Animator _bodyAnim;

    private float timer;
    private Vector2 origin;

    public int projectileAmounts { get { return projectileAmount; } set { projectileAmount = value; } }

    public GameObject currentProjectile
    {
        get { return projectile; }
        set { projectile = value; }
    }

    private void Start()
    {
        _bodyAnim = GetComponent<Animator>();
    }


    public void Attack(bool attack, bool facingLeft)
    {

        if (projectile != null)
        {

            //shoot + delay count down
            if (facingLeft)
                origin = new Vector2(gameObject.transform.position.x - gameObject.transform.localScale.x / 1.25f, gameObject.transform.position.y);
            else
                origin = new Vector2(gameObject.transform.position.x - gameObject.transform.localScale.x / -1.20f, gameObject.transform.position.y);


            projectile.GetComponent<ITrajectory>().facingLeft(facingLeft);

            if (timer <= 0)
            {
                if (GetComponent<IPlayer>() != null)
                {
                    if (projectile.name == "Sword-Projectile")
                        _bodyAnim.SetBool("Heavy", true);
                    else
                        _bodyAnim.SetBool("Heavy", false);
                }
                if (attack && projectileAmount != 0 || attack && infinite)
                {


                    if (GetComponent<IPlayer>() != null && GetComponent<Animator>() == null)
                        Debug.LogError("A animator is required to use this script");
                    else if (GetComponent<IPlayer>() != null)
                        _bodyAnim.SetBool("Throw", true);

                    else
                    {
                        Debug.Log("Else instantiate");
                        InstantiateProjectile();
                    }


                    projectileAmount -= 1;
                    timer += _endLag;
                }
            }
            else if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (GetComponent<IPlayer>() != null)
                    _bodyAnim.SetBool("Throw", false);
            }

        }
    }

    public void InstantiateProjectile()
    {
        Instantiate(projectile, origin, Quaternion.identity, gameObject.transform);
    }

}
