using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Player,
    Enemy,
    Object,
    Dummy
}

[RequireComponent(typeof(Damage))]
public class KnockBack : MonoBehaviour
{
    [Header("Knock Back Values")]
    [SerializeField] private float _knockBackForce;
    [SerializeField][Range(0, 360)] private float _knockBackAngle;
    //HAS NOT BEEN APPLIED YET. TO LAZY
    [SerializeField] private bool _flipsAngleWithEntity = true;

    [SerializeField] private LayerMask _layerMask;

    [Header("Knock Back Stun")]
    [SerializeField] private float _stunDuration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_layerMask == (_layerMask | (1 << collision.gameObject.layer)))
        {
            //Health.KnockBack
            collision.GetComponent<Health>().ApplyKnockBack(AngleFlipCheck(_knockBackAngle, collision), _knockBackForce, _stunDuration);
        }
    }

    private float AngleFlipCheck(float knockbackAngle, Collider2D collider)
    {
        float normalAngle = knockbackAngle;
        float flippedAngle = 0;
        float finalAngle = 0;
        if (Mathf.Sign(collider.transform.lossyScale.x) < 0)
        {
            if (normalAngle < 180 && normalAngle > 90 || normalAngle > 270 && normalAngle < 360)
            {
                flippedAngle = knockbackAngle - 90;
            }
            else if (normalAngle < 90 && normalAngle > 0 || normalAngle > 180 && normalAngle < 270)
            {
                flippedAngle = knockbackAngle + 90;
            }
            finalAngle = flippedAngle;
        }
        else
        {
            finalAngle = normalAngle;
        }


        return finalAngle;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(new Vector2(gameObject.transform.position.x, transform.position.y),
            new Vector2(Mathf.Cos(_knockBackAngle * Mathf.Deg2Rad),Mathf.Sin(_knockBackAngle * Mathf.Deg2Rad)));
    }
}
