using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKnife : MonoBehaviour
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
    [SerializeField] private LayerMask _layerMask;

    private RaycastHit2D _projectileRaycastR;
    private RaycastHit2D _projectileRaycastL;

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
        if (bounced)
            Spinning(spinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPlayer>() != null)
        {
            if (transform.parent.GetComponent<IEnemy>() != null)
            {
                collision.GetComponent<IHealth>().TakeDamage(1, _spR.flipX);
                Destroy(gameObject);
            }
        }
        else if (collision.GetComponent<IEnemy>() != null)
        {
            if (gameObject.transform.parent.GetComponent<IPlayer>() != null)
            {
                collision.GetComponent<IHealth>().TakeDamage(1, _spR.flipX);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) //collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (!bounced)
            {
                
                if (_spR.flipX)
                {
                    _trajectory.stopMoving = true;
                    Vector2 Angle = new Vector2(-Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
                    _rB.velocity = Angle;
                }
                else
                {
                    _trajectory.stopMoving = true;
                    Vector2 Angle = new Vector2(Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
                    _rB.velocity = Angle;
                }



                bounced = true;
            }
            else
            {
                _particles.Play();

                Destroy(gameObject, 0.3f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        
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
            Debug.Log("RIGHTSIDE");
            bounced = true;

        }
        else if(_projectileRaycastL.collider != null && !bounced)
        {
            _trajectory.stopMoving = true;
            Vector2 Angle = new Vector2(Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
            _rB.velocity = Angle;
            Debug.Log("LEFTSIDE");
            bounced = true;
        }
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.right * raycastLength);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.left * raycastLength);
    }
}
