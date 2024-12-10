using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerPhysics : MonoBehaviour, IEntityPhysics
{
    /*asmdioandisanddsandandisandaod*/
    [SerializeField] private bool _hasMomentum;

    private Rigidbody2D _rB;
    private GroundDetection _groundDetection;

    private bool _isGrounded;
    private bool _isFacingRight;

    private float _currentMax;
    private float _changingMax;

    public Rigidbody2D Rigidbody => _rB;
    public GroundDetection GroundDetection => _groundDetection;
    public bool IsGrounded => _isGrounded;
    public bool IsFacingRight => _isFacingRight;

    private Vector2 _moveInput;

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Awake()
    {
        _rB = GetComponent<Rigidbody2D>();
        _groundDetection = GetComponent<GroundDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _groundDetection.OnGround();

        _rB.velocity = new Vector2(Mathf.Clamp(_rB.velocity.x, -_changingMax, _changingMax), _rB.velocity.y);

        if(_currentMax < _changingMax)
        {
            _changingMax = Mathf.Abs(_rB.velocity.x);
        }

        if(_moveInput.x > 0)
        {
            _isFacingRight = true;
        }
        else if(_moveInput.x < 0)
        {
            _isFacingRight = false;
        }
    }

    public void SetChangingSpeed(float speed)
    {
        _changingMax = speed;
        _currentMax = speed;
    }

    public void SetCurrentMaxSpeed(float speed)
    {

        if(speed > Mathf.Abs(_changingMax))
        {
            Debug.Log("YOOO");
            _currentMax = speed;
            _changingMax = speed;
        }
        else
        {
            _currentMax = speed;
        }
    }
}
