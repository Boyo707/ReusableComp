using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    [SerializeField] GameObject _objectToFollow;
    private Vector2 _direction;
    // Start is called before the first frame update
    void Start()
    {

    }

    public Vector2 moveDir 
    {
        get { return _direction; }    
    
    }


    // Update is called once per frame
    void Update()
    {
        //checked of het links of rechts staat van de enemy
        float distance = Vector2.Distance(gameObject.transform.position, _objectToFollow.transform.position);

        float otherDistance = _objectToFollow.transform.position.x - gameObject.transform.position.x;


        if (otherDistance > 0)
            _direction = Vector2.right;
        else if (otherDistance < 0)
            _direction = Vector2.left;

    }
}
