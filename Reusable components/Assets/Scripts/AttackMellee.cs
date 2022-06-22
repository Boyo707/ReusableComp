using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMellee : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 2f)] private float boxWith;
    [SerializeField] [Range(0.0f, 2f)] private float boxLength;
    [SerializeField] [Range(-2f, 2f)] private float boxX;
    [SerializeField] [Range(-2f, 2f)] private float boxY;
    [SerializeField] private float _endLag;

    private float timer;
    private Vector2 boxOrigin;
    [SerializeField] private LayerMask _layerMask;

    private RaycastHit2D boxRayCastHit;

    [SerializeField] private Animator _bodyAnim;

    private bool _facingLeft;





    public void MelleeAttack(bool attack, bool facingLeft)
    {



        if (facingLeft)
            boxOrigin = new Vector2(transform.position.x + -1.63f, transform.position.y + -0.03f);
        else
            boxOrigin = new Vector2(transform.position.x + 1.53f, transform.position.y + -0.03f);

        _facingLeft = facingLeft;

        if (timer <= 0)
        {

            if (attack)
            {
                if (GetComponent<IPlayer>() != null)
                    _bodyAnim.SetBool("Attack", true);
                
                timer += _endLag;
            }
        }
        else if (timer > 0)
        {
            if (GetComponent<IPlayer>() != null)
                _bodyAnim.SetBool("Attack", false);

            timer -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + boxX, transform.position.y + boxY), new Vector2(boxWith,boxLength));
        Gizmos.DrawWireCube(boxOrigin, new Vector2(boxWith,boxLength));
    }

    public void activateHitBox()
    {
        Debug.Log("attacking");
        boxRayCastHit = Physics2D.BoxCast(boxOrigin, new Vector2(boxWith, boxLength), 0, Vector2.down, 0, _layerMask);
        Debug.Log(boxRayCastHit.collider);
        if (boxRayCastHit.collider != null)
        {
            Debug.Log("attack hit");
            boxRayCastHit.collider.gameObject.GetComponent<IHealth>().TakeDamage(1, _facingLeft);
        }
    }

}
