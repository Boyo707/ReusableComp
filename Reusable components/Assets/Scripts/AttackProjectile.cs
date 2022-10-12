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

                if (flipped)
                    origin = new Vector2(gameObject.transform.position.x - gameObject.transform.localScale.x / 1.25f, gameObject.transform.position.y);
                else
                    origin = new Vector2(gameObject.transform.position.x - gameObject.transform.localScale.x / -1.20f, gameObject.transform.position.y);


                if (_projectileAmount != 0 || _infinite)
                {

                    StartCoroutine(WaitingCourotine(flipped));
                    


                    _projectileAmount -= 1;
                }
            }
        }
            
        
        else
            Debug.LogError($"Unassigned Projectile in {gameObject.name}");
    }

    IEnumerator WaitingCourotine(bool flipped)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        

        GameObject thrownProjectile = (GameObject)Instantiate(_projectile, transform.position, transform.rotation);
        ITrajectory thing = thrownProjectile.GetComponent<ITrajectory>();

        thing.FacingLeft(flipped);

        yield return new WaitForSeconds(_throwDelay);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    public void InstantiateProjectile()
    {
        Instantiate(_projectile, origin, Quaternion.identity, gameObject.transform);
    }

}
