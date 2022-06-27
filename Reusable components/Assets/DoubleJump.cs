using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour, IJump
{
    private Rigidbody2D rigid;

    [SerializeField] private LayerMask _layermask = 320;
    [SerializeField] private jumpTypes _jType = jumpTypes.Advanced;
    [SerializeField] private bool _autoJump;
    [SerializeField] private float _jumpForce = 15;
    [SerializeField] private float _doubleJumpForce = 10;
    [SerializeField] private float fallMultiplier = 3;
    [SerializeField] private float lowJumpMultiplier = 5;
    [SerializeField] private bool _onGround;
    [SerializeField] private float jumpButtonGrace = 0.1f;

    private float? lastGroundTime;
    private float? jumpButtonPressedTime;

    private RaycastHit2D _jumpCast1;
    private RaycastHit2D _jumpCast2;

    private float _time;

    private bool _doubleJumped = false;

    private bool _once = true;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public bool onGround
    {
        get { return _onGround; }
    }

    public void JumpInput(bool jumpDown, bool jumpHold = false)
    {
        if (_autoJump)
        {
            _jumpCast1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.3f), Vector2.right, 0.7f, _layermask);
            _jumpCast2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.3f), Vector2.right, 0.7f, _layermask);
            if (_jumpCast1.collider != null || _jumpCast2.collider != null)
            {
                if (_once)
                {
                    rigid.velocity = Vector2.up * _jumpForce;
                    _once = false;
                }
            }
            else
                _once = true;
        }


        if (_onGround)
        {
            lastGroundTime = Time.time;
            _doubleJumped = false;
        }


        if (jumpDown)
            jumpButtonPressedTime = Time.time;

        if (Time.time - lastGroundTime <= jumpButtonGrace)
        {
            if (Time.time - jumpButtonPressedTime <= jumpButtonGrace)
            {
                Debug.Log("Jump!");
                rigid.velocity = Vector2.up * _jumpForce;
                jumpButtonPressedTime = null;
                lastGroundTime = null;
            }
        }

        if (!_doubleJumped && !_onGround && jumpDown)
        {
            rigid.velocity = Vector2.up * _doubleJumpForce;
            _doubleJumped = true;
        }

        if (_jType == jumpTypes.Advanced)
        {

            if (rigid.velocity.y < 0)
            {
                rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rigid.velocity.y > 0 && !jumpHold)
            {
                rigid.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

        }

    }

    public void GroundCheck()
    {

        Vector2 lengthN = new Vector2(transform.position.x - 0.05f, transform.position.y - 1f);
        _onGround = (Physics2D.OverlapBox(lengthN, new Vector2(1.16f, 0.08f), 0, _layermask) != null);

    }




    private void Update()
    {

    }

    void OnDrawGizmos()
    {
        Vector2 lengthN = new Vector2(transform.position.x - 0.05f, transform.position.y - 1f);


        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(lengthN, new Vector2(1.16f, 0.08f));
        //Gizmos.DrawWireCube(lengthN, new Vector2(1.10f, 0.5f));

        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f), Vector2.right * 0.7f);
        Gizmos.DrawRay(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f), Vector2.left * 0.7f);
    }
}
