using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrajectoryStraight : MonoBehaviour, ITrajectory
{
    [SerializeField] private float _speed = 20;
    [SerializeField] private float _range = 10;
    [SerializeField] private bool _facingLeft;
    private float startingPos;
    private float currentPos;
    private float endPos;

    private Rigidbody2D _rB;
    private SpriteRenderer _spR;

    public void facingLeft(bool facingLeft)
    {
        _facingLeft = facingLeft;
    }


    // Start is called before the first frame update
    void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
        _spR = GetComponent<SpriteRenderer>();
        _rB.constraints = RigidbodyConstraints2D.FreezePositionY;
        startingPos = _rB.position.x;
        endPos += startingPos + _range;
        currentPos = _rB.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_facingLeft)
        {
            _spR.flipX = true;
            endPos = startingPos - _range;
            if (currentPos > endPos)
            {
                Debug.Log("STRAIGHT LEFT");
                currentPos = _rB.position.x;
                _rB.velocity = Vector2.left * _speed;
            }
            else
            {
                _rB.velocity = new Vector2(_rB.velocity.x, _rB.velocity.y);
                _rB.constraints = RigidbodyConstraints2D.None;
            }
        }
        else if (!_facingLeft)
        {

            endPos = startingPos + _range;
            if (currentPos < endPos)
            {
                _rB.velocity = Vector2.right * _speed;
                currentPos = _rB.position.x;
            }
            else
            {
                _rB.velocity = new Vector2(_rB.velocity.x, _rB.velocity.y);
                _rB.constraints = RigidbodyConstraints2D.None;
            }

        }
    }
}
