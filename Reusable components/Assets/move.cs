using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    //[SerializeField] private float _jumpForce;
    private Vector2 _moveInput;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void entityMove(Vector2 directionInput)
    {
        _rb.velocity = new Vector2(directionInput.x * _speed * Time.deltaTime, _rb.velocity.y);
    }

    private void FixedUpdate()
    {
        /*_rb.velocity = new Vector2(_moveInput.x * _speed * Time.deltaTime, _rb.velocity.y);
        if (_moveInput.y > 0)
        {
            Debug.Log("Jump!");
            _rb.velocity = new Vector2(_moveInput.x * _speed * Time.deltaTime, _moveInput.y * _jumpForce);
        }*/
    }

    // Update is called once per frame
    void Update()
    {

        //_moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
