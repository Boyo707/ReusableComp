using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMellee : MonoBehaviour, IAttack
{
    [SerializeField] private int _damage;
    [SerializeField] [Range(0.0f, 2f)] private float boxWith;
    [SerializeField] [Range(0.0f, 2f)] private float boxLength;
    [SerializeField] [Range(-2f, 2f)] private float boxXoffset;
    [SerializeField] [Range(-2f, 2f)] private float boxX;
    [SerializeField] [Range(-2f, 2f)] private float boxY;
    [SerializeField] private float _endLag;
    [SerializeField] private float _lagOnhHit;

    private float timer;
    private Vector2 boxOrigin;
    [SerializeField] private LayerMask _layerMask;

    private RaycastHit2D boxRayCastHit;

   private Animator _bodyAnim;

    private bool _facingLeft;

    private bool _hit = false;

    public bool AttackHit
    {
        get { return _hit; }
    }


    private void Start()
    {
        _bodyAnim = GetComponent<Animator>();
    }


    public void Attack(bool attack, bool facingLeft)
    {
        if (facingLeft)
            boxOrigin = new Vector2(transform.position.x  + -boxX + boxXoffset, transform.position.y + boxY);
        else
            boxOrigin = new Vector2(transform.position.x + boxX, transform.position.y + boxY);

        _facingLeft = facingLeft;

        if (_hit)
            timer += _lagOnhHit;

        if (timer <= 0)
        {
            if (attack && _hit == false)
            {
                if (GetComponent<IPlayer>() != null)
                    _bodyAnim.SetBool("Attack", true);
                else
                    activateHitBox();
                
                timer += _endLag;
            }
        }
        else if (timer > 0)
        {
            if (GetComponent<IPlayer>() != null)
                _bodyAnim.SetBool("Attack", false);
            _hit = false;
            timer -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + boxX + boxXoffset, transform.position.y + boxY), new Vector2(boxWith,boxLength));
        Gizmos.DrawWireCube(boxOrigin, new Vector2(boxWith,boxLength));
    }

    public void activateHitBox()
    {
            boxRayCastHit = Physics2D.BoxCast(boxOrigin, new Vector2(boxWith, boxLength), 0, Vector2.down, 0, _layerMask);
            if (boxRayCastHit.collider != null)
            {
                Debug.Log("Melle attack hit");
                boxRayCastHit.collider.gameObject.GetComponent<IHealth>().TakeDamage(1, _facingLeft);
                _hit = true;
            }
        
    }

}
