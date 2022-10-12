using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    [SerializeField] private bool _flipsAngleOnSpriteFlipX = true;
    [SerializeField] private Transform _entityTransform;

    [SerializeField] private LayerMask _layerMask;

    [Header("Knock Back Stun")]
    [SerializeField] private float _stunDuration;

    private bool _flipped = false;
    private float _orignalAngle;
    private float _flippedAngle;

    void Start()
    {
        bool done = false;
        _orignalAngle = _knockBackAngle;
        if (_flipsAngleOnSpriteFlipX)
        {
            if (!done)
            {
                if (_orignalAngle < 180 && _orignalAngle > 90 && !done || _orignalAngle > 270 && _orignalAngle < 360 && !done)
                {
                    _flippedAngle = _knockBackAngle - 90;
                    done = true;

                }
                else if (_orignalAngle < 90 && _orignalAngle > 0 && !done || _orignalAngle > 180 && _orignalAngle < 270 && !done)
                {
                    _flippedAngle = _knockBackAngle + 90;
                    done = true;

                }
            }
            

        }
    }

    private void Update()
    {
        

        if (_entityTransform.localScale.x == -1)
        {
            _flipped = true;
        }
        else
        {
            _flipped = false;
        }
        if (_flipped)
            _knockBackAngle = _flippedAngle;
        else
            _knockBackAngle = _orignalAngle;



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (_layerMask == (_layerMask | (1 << collision.gameObject.layer)))
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
