using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArchTrajectory : MonoBehaviour, ITrajectory
{
    private Rigidbody2D _rB;
    [SerializeField]private float _speed = 20;
    [SerializeField]private bool _facingLeft;
    [SerializeField]private float _arch = 45;
    // Start is called before the first frame update
    void Start()
    {
        _rB = GetComponent<Rigidbody2D>();

    }

    public void FacingLeft(bool facingLeft)
    {
        _facingLeft = facingLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if (_rB.velocity.x == 0)
        { 
            Vector2 Angle = Vector2.zero;
            if (_facingLeft)
                Angle = new Vector2(Mathf.Cos((_arch + 180) * Mathf.Deg2Rad) * _speed, Mathf.Sin(_arch * Mathf.Deg2Rad) * _speed);
            else if (!_facingLeft)
                Angle = new Vector2(Mathf.Cos(_arch * Mathf.Deg2Rad) * _speed, Mathf.Sin(_arch * Mathf.Deg2Rad) * _speed);
            _rB.velocity = Angle;

        }
        Vector2 v = _rB.velocity;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnDrawGizmos()
    {

    }
}
