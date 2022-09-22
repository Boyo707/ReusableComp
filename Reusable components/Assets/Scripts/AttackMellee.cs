using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMellee : MonoBehaviour, IAttack
{



    ///<Summary>
    /// Inspiratie video voor de attack scripts HEEL SIMPEL!?!??!?<see href="https://youtu.be/Xw8F57Ci_b8?t=393">HERE</see>
    /// Ook een functie maken dat als de colliders iets hitten dat ze uit gaan.
    /// Ook een functie voor de active duration.
    ///</Summary>

    [SerializeField] private GameObject _attack1;
    [SerializeField] private float _endLag;
    [SerializeField] private float _lagOnhHit;

    [SerializeField] private LayerMask _layerMask;

   private Animator _bodyAnim;

    private bool _facingLeft;

    private bool _hit = false;

    public bool AttackHit
    {
        get { return _hit; }
    }


    private void Start()
    {
        //_bodyAnim = GetComponent<Animator>();
    }


    public void Attack(bool attack)
    {
        if (attack)
        {
            _attack1.SetActive(true);
            Debug.Log("attack");
        }
        else
        {
            _attack1.SetActive(false);
        }
        /*if (facingLeft)
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
        }*/
    }

    public void activateHitBox()
    {
            /*boxRayCastHit = Physics2D.BoxCast(boxOrigin, new Vector2(boxWith, boxLength), 0, Vector2.down, 0, _layerMask);
            if (boxRayCastHit.collider != null)
            {
                Debug.Log("Melle attack hit");
                //boxRayCastHit.collider.gameObject.GetComponent<IHealth>().TakeDamage(1, _facingLeft);
                _hit = true;
            }*/
        
    }

}
