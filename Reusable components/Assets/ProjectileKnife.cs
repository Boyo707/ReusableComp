using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKnife : MonoBehaviour, IProjectile
{
    private Rigidbody2D _rB;
    private SpriteRenderer _spR;
    private ParticleSystem _particles;
    private ProjectileTrajectoryStraight _trajectory; 

    [SerializeField] private float arch;
    [SerializeField] private float speed;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float _spinDelay;
    [SerializeField] private float raycastLength;
    [SerializeField] private float raycastLengthDown;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _particle;

    private GameObject _parent;

    private RaycastHit2D _projectileRaycastR;
    private RaycastHit2D _projectileRaycastL;

    private RaycastHit2D _projectileRaycastB;

    private bool bounced = false;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = _spinDelay;
        _rB = GetComponent<Rigidbody2D>();
        _spR = GetComponent<SpriteRenderer>();
        _particles = GetComponent<ParticleSystem>();
        _trajectory = GetComponent<ProjectileTrajectoryStraight>();
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
        GroundCheck();
        if (bounced)
            Spinning(spinSpeed);
    }


    public GameObject itParent
    {
        get { return _parent; }
        set { _parent = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponentInParent<IEnemy>() != null)
        {
            if (collision.GetComponent<IPlayer>() != null)
            {
                //collision.GetComponent<IHealth>().TakeDamage(1, _spR.flipX);
                Vector2 lastLocation = gameObject.transform.position;
                Instantiate(_particle, lastLocation, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else if (GetComponentInParent<IPlayer>() != null)
        {
            if (collision.GetComponent<IEnemy>() != null)
            {
                //collision.GetComponent<IHealth>().TakeDamage(1, _spR.flipX);
                Vector2 lastLocation = gameObject.transform.position;
                Instantiate(_particle, lastLocation, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        /*else if (collision.gameObject.layer == _layerMask) //collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("MADE COLLSION MADE COLLISION MADE COLLISION MADE COLLISION MADE COLLSION");
            if (!bounced)
            {
                
                if (_spR.flipX)
                {
                    Debug.Log("==========FLIPPED====================");
                    _trajectory.stopMoving = true;
                    Vector2 Angle = new Vector2(-Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
                    _rB.velocity = Angle;
                }
                else
                {
                    Debug.Log("=============NotFLIPPED==============");
                    _trajectory.stopMoving = true;
                    Vector2 Angle = new Vector2(Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
                    _rB.velocity = Angle;
                }

                Debug.Log("=====================================================");

                bounced = true;
            }
            else
            {
                Vector2 lastLocation = gameObject.transform.position;
                Instantiate(_particle, lastLocation, Quaternion.identity);

                Destroy(gameObject, 0.3f);
            }
        }*/
    }

    public void Spinning(float speed)
    {
        if (timer <= 0)
        {
            if(_spR.flipX)
                transform.Rotate(0.0f, 0.0f, -speed * Time.deltaTime, Space.World);
            else
                transform.Rotate(0.0f, 0.0f, speed * Time.deltaTime, Space.World);

        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;

        }
    }

    private void WallCheck()
    {
        //Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - 1f);
        _projectileRaycastR = Physics2D.Raycast(transform.position, Vector2.right, raycastLength, _layerMask);
        _projectileRaycastL = Physics2D.Raycast(transform.position, Vector2.left, raycastLength, _layerMask);

        if (_projectileRaycastR.collider != null && !bounced)
        {
            _trajectory.stopMoving = true;
            Vector2 Angle = new Vector2(-Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
            _rB.velocity = Angle;
            bounced = true;

        }
        else if(_projectileRaycastL.collider != null && !bounced)
        {
            _trajectory.stopMoving = true;
            Vector2 Angle = new Vector2(Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
            _rB.velocity = Angle;
            bounced = true;
        }
    }

    private void GroundCheck()
    {
        //Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - 1f);
        _projectileRaycastB = Physics2D.Raycast(transform.position, Vector2.down, raycastLengthDown, _layerMask);

        if (_projectileRaycastB.collider != null && !bounced)
        {
            if (_spR.flipX)
            {
                Debug.Log("==========FLIPPED====================");
                _trajectory.stopMoving = true;
                Vector2 Angle = new Vector2(-Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
                _rB.velocity = Angle;
                bounced = true;
            }
            else
            {
                Debug.Log("=============NotFLIPPED==============");
                _trajectory.stopMoving = true;
                Vector2 Angle = new Vector2(Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
                _rB.velocity = Angle;
                
            }
            bounced = true;
        }
        
    }

    public void setParent(GameObject parent)
    {
        _parent = parent;
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.right * raycastLength);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.left * raycastLength);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.down * raycastLengthDown);
    }
}
