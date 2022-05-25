using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum projectileTrajectory
{
    Straight,
    Arched
}


public class Projectile : MonoBehaviour, IProjectile
{
    private Rigidbody2D _rB;
    private SpriteRenderer _spR;

    [SerializeField] private projectileTrajectory _pT;
    [SerializeField] private bool _friendlyProjectile;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _range = 5;
    [SerializeField] private float _arch = 0;
    [SerializeField] private float _lifeTime;
    [SerializeField] private bool _destroysOnHit;
    

    [SerializeField]private bool _facingLeft;

    private float startingPos;
    private float currentPos;
    private float endPos;
    void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
        _spR = GetComponent<SpriteRenderer>();


        if (_pT == projectileTrajectory.Straight)
        {
            _rB.constraints = RigidbodyConstraints2D.FreezePositionY;
            startingPos = _rB.position.x;
            endPos += startingPos + _range;
            currentPos = _rB.position.x;

        }
    }

    private void Awake()
    {
        
    }

    public void FacingLeft(bool left)
    {
        _facingLeft = left;
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileSettings(_speed, _facingLeft, _arch);

        if(_lifeTime != 0)
            Destroy(gameObject, _lifeTime);
    }

    

    public void ProjectileSettings(float speed, bool facingLeft, float arch)
    {
        switch (_pT)
        {
            case projectileTrajectory.Arched:
                Arched(speed, facingLeft, arch);
                break;

            case projectileTrajectory.Straight:
                Straight(speed, facingLeft);
                break;
        }

    }


    private void Arched(float speed, bool facingLeft, float arch)
    {
        if (_rB.velocity.x == 0)
        {

            Vector2 Angle = Vector2.zero;
            if (facingLeft)
                Angle = new Vector2(Mathf.Cos((arch + 180) * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
            else if (!facingLeft)
                Angle = new Vector2(Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
            _rB.velocity = Angle;

        }
        Vector2 v = _rB.velocity;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Straight(float speed, bool facingLeft)
    {

    
        if (facingLeft)
        {
            _spR.flipX = true;
            endPos = startingPos - _range;
            if (currentPos > endPos)
            {
                Debug.Log("STRAIGHT LEFT");
                currentPos = _rB.position.x;
                _rB.velocity = Vector2.left * speed;
            }
            else
            {
                _rB.velocity = new Vector2(_rB.velocity.x, _rB.velocity.y);
                _rB.constraints = RigidbodyConstraints2D.None;
            }
        }
        else if (!facingLeft)
        {
            
            endPos = startingPos + _range;
            if (currentPos < endPos)
            {
                _rB.velocity = Vector2.right * speed;
                currentPos = _rB.position.x;
            }
            else
            {
                _rB.velocity = new Vector2(_rB.velocity.x, _rB.velocity.y);
                _rB.constraints = RigidbodyConstraints2D.None;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_destroysOnHit)
        {
            
            if (_friendlyProjectile)
            {
                if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                    Debug.Log("COllision enemy");
                }
            }
            else
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    //class for damage?
                }
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Destroy(gameObject);
            }
        }

    }


        //aparte scipt voor maken. 
        public void Spinning(float speed)
    {
        transform.Rotate(0.0f, 0.0f, speed * Time.deltaTime, Space.World);
    }


}
