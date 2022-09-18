using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpear : MonoBehaviour, IEnemy
{
    [SerializeField] GameObject _particle;
    private SpriteRenderer _spR;

    // Start is called before the first frame update
    void Start()
    {
        _spR = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPlayer>() != null)
        {
            if (GetComponentInParent<IEnemy>() != null)
            {
                //collision.GetComponent<IHealth>().TakeDamage(2, _spR.flipX);
                Vector2 lastLocation = gameObject.transform.position;
                Instantiate(_particle, lastLocation, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else if (collision.GetComponent<IEnemy>() != null)
        {
            if (GetComponentInParent<IPlayer>() != null)
            {
                //collision.GetComponent<IHealth>().TakeDamage(2, _spR.flipX);
                Vector2 lastLocation = gameObject.transform.position;
                Instantiate(_particle, lastLocation, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) //collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {

            Vector2 lastLocation = gameObject.transform.position;
            Instantiate(_particle, lastLocation, Quaternion.identity);

            Destroy(gameObject, 0.3f);
        }
    }
}
