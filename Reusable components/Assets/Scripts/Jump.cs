using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum jumpTypes
{
    Basic,
    Advanced
}


public class Jump : MonoBehaviour, IJump
{

    private Rigidbody2D rigid;

    [SerializeField]private LayerMask _layermask;
    [SerializeField]private jumpTypes _jType;
    [SerializeField]private float _jumpForce;
    [SerializeField]private float fallMultiplier;
    [SerializeField]private float lowJumpMultiplier;
    [SerializeField]private bool _onGround;
    [SerializeField] private float jumpButtonGrace;

    private float? lastGroundTime;
    private float? jumpButtonPressedTime;
    

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

        if (_onGround)
            lastGroundTime = Time.time;

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
        /*if (jumpDown && _onGround)
        {
            Debug.Log("Jump!");
            rigid.velocity = Vector2.up * _jumpForce;
        }*/

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
        
            
    }
}
