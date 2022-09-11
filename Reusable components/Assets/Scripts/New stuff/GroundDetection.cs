using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundDetection : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private bool _onGround;


    [Header("Detection Box Settings")]
    [Range(-2, 2)]
    [SerializeField] private float _boxYPosition;

    [Range(-2, 2)]
    [SerializeField] private float _boxXPosition;

    [Range(0, 2)]
    [SerializeField] private float _boxWidth;

    [Range(0, 2)]
    [SerializeField] private float _boxHeight;

    [SerializeField] private Color _colorOnGround = Color.green;
    [SerializeField] private Color _colorOffGround = Color.red;


    private RaycastHit2D blah;

    private void Update()
    {
        _onGround = OnGround();
    }

    public bool OnGround()
    {
        return _onGround = (Physics2D.OverlapBox(new Vector2(transform.position.x + _boxXPosition, transform.position.y + _boxYPosition), new Vector2(_boxWidth, _boxHeight), 0, _groundLayerMask) != null);
    }

    private void OnDrawGizmosSelected()
    {

        if(_onGround)
            Gizmos.color = _colorOnGround;
        else
            Gizmos.color = _colorOffGround;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + _boxXPosition,transform.position.y + _boxYPosition), new Vector2(_boxWidth, _boxHeight));

    }
}
