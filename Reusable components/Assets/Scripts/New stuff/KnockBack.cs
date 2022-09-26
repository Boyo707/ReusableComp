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
    //[SerializeField] private float _knockBackLayerValue;
    [SerializeField] private float _knockBackForce;
    [SerializeField][Range(0, 360)] private float _knockBackAngle;
    [SerializeField] private bool _flipsAngleOnSpriteFlipX = true;
    [SerializeField] private Transform _entityTransform;

    [SerializeField] private LayerMask _layerMask;
    //[SerializeField] private string[] _knockBackLayers;
    //[SerializeField] private ObjectType _type;

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
            if (_flipped)
            {
                if (!done)
                {
                    if (_orignalAngle < 90 && _orignalAngle > 0 && done || _orignalAngle < 180 && _orignalAngle > 90 && done)
                    {
                        _flippedAngle = 180 - _knockBackAngle;
                        done = true;
                        Debug.Log("I flipped 1");
                    }

                    else if (_orignalAngle > 180 && _orignalAngle < 270 && done || _orignalAngle > 270 && _orignalAngle < 360 && done)
                    {
                        _flippedAngle = 360 - (_knockBackAngle - 180);
                        done = true;
                        Debug.Log("I flipped 2");
                    }
                }
            }
                
        }
        _flippedAngle = 180 - 34;
        Debug.Log(_flippedAngle);
        Debug.Log(_orignalAngle);
    }

    private void Update()
    {
        if (_entityTransform.localScale.x == -1)
            _flipped = true;
        else
            _flipped = false;

        Debug.Log(_entityTransform.lossyScale.x);

        if (_flipped)
            _knockBackAngle = _flippedAngle;
        else
            _knockBackAngle = _orignalAngle;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool doneOnce = false;
        
        


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
