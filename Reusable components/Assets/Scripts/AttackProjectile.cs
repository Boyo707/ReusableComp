using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject _projectile;

    [Header("Settings")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _infinite;
    [SerializeField] private int _projectileAmount;
    [SerializeField] private float _throwDelay;

    

    private Animator _bodyAnim;

    private float timer = 1;
    private Vector2 origin;

    public int projectileAmounts { get { return _projectileAmount; } set { _projectileAmount = value; } }

    public GameObject currentProjectile
    {
        get { return _projectile; }
        set { _projectile = value; }
    }

    private void Start()
    {
        _bodyAnim = GetComponent<Animator>();
    }


    public void Attack(bool attack)
    {

        if (_projectile != null)
        {
            if (attack)
            {
                bool flipped = false;
                Transform objectTransform = gameObject.transform;

                if (gameObject.transform.localScale.x == -1)
                    flipped = true;


                Debug.Log(objectTransform);
                Debug.Log(flipped);


                //shoot + delay count down
                if (flipped)
                    origin = new Vector2(gameObject.transform.position.x - gameObject.transform.localScale.x / 1.25f, gameObject.transform.position.y);
                else
                    origin = new Vector2(gameObject.transform.position.x - gameObject.transform.localScale.x / -1.20f, gameObject.transform.position.y);


                //_projectile.GetComponent<ITrajectory>().FacingLeft(flipped);

                /*if (timer <= 0)
                {*/
                /*if (GetComponent<IPlayer>() != null)
                {
                    if (_projectile.name == "Sword-Projectile")
                        _bodyAnim.SetBool("Heavy", true);
                    else
                        _bodyAnim.SetBool("Heavy", false);
                }*/
                if (_projectileAmount != 0 || _infinite)
                {


                    /*if (GetComponent<IPlayer>() != null && GetComponent<Animator>() == null)
                        Debug.LogError("A animator is required to use this script");
                    else if (GetComponent<IPlayer>() != null)
                        _bodyAnim.SetBool("Throw", true);*/
                    Debug.Log("Else instantiate");
                    
                    //InstantiateProjectile();
                    /*else
                    {
                       //had debug.log and instantiate in it. 
                    }*/


                    _projectileAmount -= 1;
                }
            }
            /*else if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (GetComponent<IPlayer>() != null)
                    _bodyAnim.SetBool("Throw", false);
            }*/
        }
            
        
        else
            Debug.LogError($"Unassigned Projectile in {gameObject.name}");
    }

    IEnumerator WaitingCourotine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(_throwDelay);

        GameObject thrownProjectile = (GameObject)Instantiate(_projectile, transform.position, transform.rotation);
        ITrajectory thing = thrownProjectile.GetComponent<ITrajectory>();

        thing.FacingLeft();

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    public void InstantiateProjectile()
    {
        Instantiate(_projectile, origin, Quaternion.identity, gameObject.transform);
    }

}
