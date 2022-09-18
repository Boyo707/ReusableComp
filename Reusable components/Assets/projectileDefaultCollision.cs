using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileDefaultCollision : MonoBehaviour
{
    private SpriteRenderer _spr;

    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPlayer>() != null)
        {
            if (transform.parent.GetComponent<IEnemy>() != null)
            {
                //collision.GetComponent<IHealth>().TakeDamage(1, _spr.flipX);
                Destroy(gameObject);
                Debug.Log("COllision player");
            }
        }
        else if (collision.GetComponent<IEnemy>() != null)
        {
            if (gameObject.transform.parent.GetComponent<IPlayer>() != null)
            {
                //collision.GetComponent<IHealth>().TakeDamage(1, _spr.flipX);
                Destroy(gameObject);
                Debug.Log("COllision enemy");
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }


    }
}
