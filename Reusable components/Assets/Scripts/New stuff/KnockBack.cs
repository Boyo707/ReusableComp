using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damage))]
public class KnockBack : MonoBehaviour
{
    [Header("Knock Back Values")]
    [SerializeField] private float _knockBackLayerValue;
    [SerializeField] private float _knockBackForce;
    [SerializeField][Range(0, 360)] private float _knockBackAngle;
    [SerializeField] private bool _flipsAngleOnSpriteFlipX = true;
    [SerializeField] private Transform _entityTransform;

    [Header("Knock Back Stun")]
    [SerializeField] private float _stunDuration;
    
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool doneOnce = false;

        if (_flipsAngleOnSpriteFlipX)
        {
            if (!doneOnce)
            {
                if (_entityTransform.localScale.x == -1)
                {
                    if (_knockBackAngle < 90 && _knockBackAngle > 0 || _knockBackAngle < 180 && _knockBackAngle > 90)
                    {
                        _knockBackAngle = 180 - _knockBackAngle;
                        doneOnce = true;
                    }
                    else if (_knockBackAngle > 180 && _knockBackAngle < 270 || _knockBackAngle > 270 && _knockBackAngle < 360)
                    {
                        _knockBackAngle = 360 - (_knockBackAngle - 180);
                        doneOnce = true;
                    }
                }
            }
        }
        
        if (collision.gameObject.layer == _knockBackLayers.value)
        {
            collision.GetComponent<IEntityController>().DisableEntityControlls(_stunDuration);

            var Angle = new Vector2(Mathf.Cos(_knockBackAngle * Mathf.Deg2Rad) * _knockBackForce,
                Mathf.Sin(_knockBackAngle * Mathf.Deg2Rad) * _knockBackForce);
            collision.GetComponent<Rigidbody2D>().velocity = Angle;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(new Vector2(gameObject.transform.position.x, transform.position.y),
            new Vector2(Mathf.Cos(_knockBackAngle * Mathf.Deg2Rad),Mathf.Sin(_knockBackAngle * Mathf.Deg2Rad)));
    }
}
