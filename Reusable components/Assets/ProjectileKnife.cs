using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKnife : MonoBehaviour
{
    private Rigidbody2D _rB;

    [SerializeField] private float arch;
    [SerializeField] private float speed;
    [SerializeField] private float spinSpeed;

    private bool bounced = false;
    // Start is called before the first frame update
    void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bounced)
            Spinning(spinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPlayer>() != null)
        {
            if (transform.parent.GetComponent<IEnemy>() != null)
            {
                collision.GetComponent<IHealth>().TakeDamage(1);
                Destroy(gameObject);
                Debug.Log("COllision player");
            }
        }
        else if (collision.GetComponent<IEnemy>() != null)
        {
            if (gameObject.transform.parent.GetComponent<IPlayer>() != null)
            {
                collision.GetComponent<IHealth>().TakeDamage(1);
                Destroy(gameObject);
                Debug.Log("COllision enemy");
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) //collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (!bounced)
            {
                Vector2 Angle = new Vector2(Mathf.Cos(arch * Mathf.Deg2Rad) * speed, Mathf.Sin(arch * Mathf.Deg2Rad) * speed);
                //_rB.velocity = new Vector2(range + speed * Time.deltaTime, _rB.velocity.y);
                _rB.velocity = Angle;
                
                bounced = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void Spinning(float speed)
    {
        transform.Rotate(0.0f, 0.0f, speed * Time.deltaTime, Space.World);
    }

}
