using UnityEngine;

public enum MovementStatesTut
{
    Velocity,
    AddForce,
    TransformPosition,
    RigidbodyMovePosition
}


public class BasicMovement : MonoBehaviour
{
    private MovementStatesTut _state; 

    private Rigidbody2D _rigidbody2D;

    public float speedFloat;

    private bool _turnOnGravity = false;

    // Start is called before the first frame update
    void Start()
    {
        _state = MovementStatesTut.Velocity;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Beweging State Veranderen
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextState();
        }

        if (Input.GetKeyDown(KeyCode.E))
            _turnOnGravity = true;


        //Gravity aan en uitzetten
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_rigidbody2D.gravityScale == 0)
            {
                _rigidbody2D.gravityScale = 1;
                _turnOnGravity = true;
            }
            else
            {
                _rigidbody2D.gravityScale = 0;
                _turnOnGravity = false;
            }
        }

        if (_turnOnGravity && Input.GetKeyDown(KeyCode.Space))
            _rigidbody2D.velocity = transform.up * speedFloat;

        //If statement voor de transform state verandering
        if (_state == MovementStatesTut.TransformPosition)
            TransformPosition();
    }

    private void FixedUpdate()
    {
        //Switch statement dat voor alle physics based components gelden.
        //Je gebruikt FixedUpdate vooral voor functies dat physics gebruikt zoals de rigidbody.
        switch (_state)
        {
            case MovementStatesTut.Velocity:
                Velocity();
                break;
            case MovementStatesTut.AddForce:
                AddForce();
                break;
            case MovementStatesTut.RigidbodyMovePosition:
                RigidbodyMovePosition();
                break;
        }
    }


    //Snelle void voor het switchen naar de volgende state.
    private void NextState()
    {
        if (_state == MovementStatesTut.Velocity)
            _state = MovementStatesTut.AddForce;
        else if (_state == MovementStatesTut.AddForce)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _state = MovementStatesTut.TransformPosition;
        }
        else if (_state == MovementStatesTut.TransformPosition)
            _state = MovementStatesTut.RigidbodyMovePosition;
        else if (_state == MovementStatesTut.RigidbodyMovePosition)
            _state = MovementStatesTut.Velocity;
    }

    private void Velocity()
    {
        //veranderd de velocity op 1 as

        if (!_turnOnGravity)
            _rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speedFloat, Input.GetAxisRaw("Vertical") * speedFloat);
        else
            _rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speedFloat, _rigidbody2D.velocity.y);

    }

    private void AddForce()
    {

        _rigidbody2D.AddForce(Input.GetAxisRaw("Horizontal") * speedFloat * transform.right);
        _rigidbody2D.AddForce(Input.GetAxisRaw("Vertical") * speedFloat * transform.up);
    }

    private void TransformPosition()
    {

        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * speedFloat * Time.deltaTime, Input.GetAxisRaw("Vertical") * speedFloat * Time.deltaTime, 0);
    }

    private void RigidbodyMovePosition()
    {

        _rigidbody2D.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * speedFloat * Time.deltaTime, Input.GetAxisRaw("Vertical") * speedFloat * Time.deltaTime, 0));
        
    }
}
