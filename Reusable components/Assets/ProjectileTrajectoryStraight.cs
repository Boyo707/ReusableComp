using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrajectoryStraight : MonoBehaviour, ITrajectory
{
    [SerializeField] private float _speed = 20;
    [SerializeField] private float _angle = 0;
    [SerializeField] private float _range = 10;
    [SerializeField] private bool _facingLeft;
    private float startingPos;
    private float currentPos;
    private float endPos;
    private bool _stopMoving = false;

    private Rigidbody2D _rB;
    private SpriteRenderer _spR;

    Vector2 Angle = Vector2.zero;

    private float _startGravity;

    //iets maken dat de update stopt of

    public void facingLeft(bool facingLeft)
    {
        _facingLeft = facingLeft;
    }

    public bool stopMoving
    {
        set { _stopMoving = value; }
    }

    public float ProjectileAngle
    {
        get { return _angle; }
        set { _angle = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
        _spR = GetComponent<SpriteRenderer>();
        _startGravity = _rB.gravityScale;
        _rB.gravityScale = 0;
        startingPos = _rB.position.x;
        endPos += startingPos + _range;
        currentPos = _rB.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_facingLeft)
        {
            
            endPos = startingPos - _range;
            if (currentPos > endPos && _stopMoving == false)
            {
                _spR.flipX = true;
                Debug.Log("STRAIGHT LEFT");
                currentPos = _rB.position.x;
                Angle = new Vector2(Mathf.Cos((_angle + 180) * Mathf.Deg2Rad) * _speed, Mathf.Sin(_angle * Mathf.Deg2Rad) * _speed);
                _rB.velocity = Angle;
                //_rB.velocity = Vector2.left * _speed;
            }
            else
            {
                _rB.velocity = new Vector2(_rB.velocity.x, _rB.velocity.y);
                _rB.gravityScale = _startGravity;
            }
        }
        else if (!_facingLeft)
        {

            endPos = startingPos + _range;
            if (currentPos < endPos && _stopMoving == false)
            {
                currentPos = _rB.position.x;
                Angle = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad) * _speed, Mathf.Sin(_angle * Mathf.Deg2Rad) * _speed);
                _rB.velocity = Angle;
            }
            else
            {
                _rB.velocity = new Vector2(_rB.velocity.x, _rB.velocity.y);
                _rB.gravityScale = _startGravity;
            }

        }
    }
}
