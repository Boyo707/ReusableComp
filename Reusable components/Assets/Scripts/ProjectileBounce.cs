using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBounce : MonoBehaviour
{
    private Rigidbody2D _rB;
    // Start is called before the first frame update
    void Start()
    {

        _rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) //collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("Touched a rail");
            float arch = 135;
            float speed = 20;
            Vector2 Angle = new Vector2(Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
            //_rB.velocity = new Vector2(range + speed * Time.deltaTime, _rB.velocity.y);
            _rB.velocity = Angle;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

    }
}
