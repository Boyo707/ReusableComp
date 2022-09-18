using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSword : MonoBehaviour
{
    //public delegate void OnInteraction();
    // Start is called before the first frame update

    //when projectile hits enemy ontrigger enter. 2 damage.
    //When projectile stay is hallway into ground (raycast?) remove rigidbody?
    //when projectile inground. Make it interactable or give it a pickup fucntion.
    //I interactable maken? 
    //UI voor pickup maken met als text PICK UP "P" zo iets

    [SerializeField] private int _damage;
    [SerializeField] private float raycastLength;
    [SerializeField] private LayerMask _layerMask;

    private RaycastHit2D _middle;
    private RaycastHit2D _left;
    private RaycastHit2D _right;

    private ParticleSystem _particle;
    private SpriteRenderer _spr;

    private bool once = false;

    private float _delayTime = 0.5f;

    private void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        _spr = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        _middle = Physics2D.Raycast(new Vector2(transform.position.x - 0.1f, transform.position.y), Vector2.down, raycastLength, _layerMask);
        _left = Physics2D.Raycast(new Vector2(transform.position.x - 0.1f, transform.position.y), Vector2.left, raycastLength, _layerMask);
        _right = Physics2D.Raycast(new Vector2(transform.position.x - 0.1f, transform.position.y), Vector2.right, raycastLength, _layerMask);
        if (_delayTime <= 0)
        {
            if (_middle.collider != null && once == false)
            {

                deactiveAndAdd();
            }
            else if (_left.collider != null && once == false)
            {
                deactiveAndAdd();
            }
            else if(_right.collider != null && once == false)
            {
                deactiveAndAdd();
            }
        }
        else
        {
            _delayTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!once)
        {
            if (GetComponentInParent<IPlayer>() != null)
            {
                if (collision.GetComponent<IEnemy>() != null)
                {
                    //collision.GetComponent<IHealth>().TakeDamage(_damage, _spr.flipX);
                }
            }
            else if (GetComponentInParent<IEnemy>() != null)
            {
                if (collision.GetComponent<IPlayer>() != null)
                {
                    //collision.GetComponent<IHealth>().TakeDamage(_damage, _spr.flipX);
                }
            }
        }
    }

    private void deactiveAndAdd()
    {
        _particle.Play();
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<ProjectileArchTrajectory>());
        gameObject.transform.parent = null;
        gameObject.AddComponent<PickUp>();

        GetComponent<PickUp>().Amount = 1;
        GetComponent<PickUp>().PickUpType = pickupTypes.Projectile;
        GetComponent<PickUp>().ProjectileType = projectileType.Sword;

        once = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y), Vector2.down * raycastLength);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector2(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y), Vector2.left * raycastLength);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Vector2(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y + 0.5f), Vector2.right * raycastLength);
    }
}
