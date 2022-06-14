using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slopeMovement : MonoBehaviour, IMovements
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private float _force;
    [SerializeField] private bool _onSlope;
    [SerializeField][Range(0.0f, 0.20f)] private float raycastLength;
    [SerializeField][Range(0.0f, 1.5f)] private float boxWith;
    [SerializeField][Range(0.0f, 1f)] private float boxLength;
    [SerializeField] private float _defaultSpeed;
    private float _currentSpeed;
    [SerializeField] private float _sprintSpeed;

    private Rigidbody2D _rb;

    private Vector2 moveDirection;
    private SpriteRenderer _spR;

    private float _defaultGravity;

    private RaycastHit2D currentHit;

    private RaycastHit2D[] boxRayCastHit;
    private RaycastHit2D rayCastHit1;


    // Start is called before the first frame update

    public bool onSlope
    {
        get { return OnSlope(); }
    }

    void Start()
    {
        _spR = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _defaultGravity = _rb.gravityScale;
    }
    private bool OnSlope()
    {
        Vector2 boxSize = new Vector2(boxWith, boxLength);
        Vector2 boxOrigin = new Vector2(transform.position.x - 0.05f, transform.position.y - 1f);
        //boxRayCastHit = Physics2D.BoxCast(boxOrigin, boxSize, 0, Vector2.down, 0, _layerMask);
        boxRayCastHit = Physics2D.BoxCastAll(boxOrigin, boxSize, 0, Vector2.down, 0, _layerMask);

        //RAYCAST HIT PAS ALS IK OP SLOPE STA EN NIET ALS RAYCAST ER OP STAAT.
        //verbetering de raycast pakt de eerste die het raakt en raak daardor niet de slope die het er naast staat.
        //Kan de slopemovement beter maken door de movement type dat in de player controller zit door de nummer te sturen door -
        //een check te sturen dat als de angle meer dan de maxSlope is door de movement type dan te veranderen.
        //kan dit soort van doen door boxcast all te doen?


        for (int i = 0; i < boxRayCastHit.Length; i++)
        {
            if (boxRayCastHit[i].collider != null)
            {
                currentHit = boxRayCastHit[i];
                float angle = Vector2.Angle(Vector2.down, currentHit.normal);
                //Debug.Log(angle);
                if (angle == 90 || angle == 180)
                {

                    return false;
                }
                   
                return angle < maxSlopeAngle && angle != 0;
            }
        }
        
       
        return false;
    }

    private Vector2 GetSlopeMoveDirection(Vector2 directionInput)
    {
        Vector3 vector3 = Vector3.ProjectOnPlane(directionInput, currentHit.normal).normalized;
        return new Vector2(vector3.x, vector3.y);
    }

    public void MoveInput(Vector2 inputDirection, bool sprinting = false)
    {
        moveDirection = new Vector2(inputDirection.x, 0);
        
        if (OnSlope())
        {
            _rb.velocity = GetSlopeMoveDirection(moveDirection) * _currentSpeed;
        }


        if (sprinting == true)
            _currentSpeed = _sprintSpeed + _defaultSpeed;
        else
            _currentSpeed = _defaultSpeed;

        if (_rb.velocity.x > 0)
        {
            _spR.flipX = false;
        }
        else if (_rb.velocity.x < 0)
        {
            _spR.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _onSlope = OnSlope();
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x - 0.05f, transform.position.y - 1f), new Vector2(boxWith, boxLength));
    }
}
