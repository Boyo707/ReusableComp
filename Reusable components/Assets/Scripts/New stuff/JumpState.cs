using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(GroundDetection))]
public class JumpState : MonoBehaviour
{

    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 15;
    [SerializeField] private float fallMultiplier = 3;
    [SerializeField] private float lowJumpMultiplier = 5;

    [Header("Coyote Jump")]
    [SerializeField] private float jumpButtonGrace = 0.1f;

    [Header("Misc")]
    [SerializeField] private bool _autoJump;
    [SerializeField] private float _doubleJumpAmount;


    private float? lastGroundTime;
    private float? jumpButtonPressedTime;

    private float temp;

    bool _jumpDown;
    bool _jumpHold = false;

    private bool _isGrounded => GetComponent<GroundDetection>().OnGround();
    private Rigidbody2D _rB => GetComponent<Rigidbody2D>();

    public void OnJump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            jumpButtonPressedTime = Time.time;
        }

        _jumpHold = callbackContext.performed;
    }

    private void Start()
    {
        temp = _doubleJumpAmount;
    }

    private void Update()
    {
        
        if (_isGrounded)
        {
            _doubleJumpAmount = temp;
            lastGroundTime = Time.time;
        }



        if (Time.time - lastGroundTime <= jumpButtonGrace)
        {
            if (Time.time - jumpButtonPressedTime <= jumpButtonGrace )
            {
                _rB.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                jumpButtonPressedTime = null;
                lastGroundTime = null;
            }
            else
            {
                jumpButtonPressedTime = null;
                lastGroundTime = null;
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(_jumpDown + " = down, " + _jumpHold + " = hold");

        if (!_isGrounded)
        {
            if (_rB.velocity.y < 0)
            {
                _rB.velocity += (fallMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
            }
            else if (_rB.velocity.y > 0 && !_jumpHold)
            {
                _rB.velocity += (lowJumpMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
            }
        }
    }

}
