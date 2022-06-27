using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArrow : MonoBehaviour
{

    private ProjectileTrajectoryStraight _trajectory;
    private EnemyArcherController _parentController;
    private SpriteRenderer _spR;
    [SerializeField]private GameObject _particle;
    // Start is called before the first frame update
    void Start()
    {
        _trajectory = GetComponent<ProjectileTrajectoryStraight>();
        _parentController = GetComponentInParent<EnemyArcherController>();
        _spR = GetComponent<SpriteRenderer>();
        //het idee was om van 2 vectors de angle te berekenen. Kan met vector2.Angle maar ik krijg altijd dat de values hellemaal fout zijn
    }

    // Update is called once per frame
    void Update()
    {

        /*Debug.Log(Vector2.SignedAngle(_parentController.TargetTransform.position, _parentController.ParentTransform.position));*/
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPlayer>() != null)
        {
            if (GetComponentInParent<IEnemy>() != null)
            {
                collision.GetComponent<IHealth>().TakeDamage(1, _spR.flipX);
                Vector2 lastLocation = gameObject.transform.position;
                Instantiate(_particle, lastLocation, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else if (collision.GetComponent<IEnemy>() != null)
        {
            if (GetComponentInParent<IPlayer>() != null)
            {
                collision.GetComponent<IHealth>().TakeDamage(1, _spR.flipX);
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
