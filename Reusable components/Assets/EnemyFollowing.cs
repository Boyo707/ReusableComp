using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    private Vector2 _direction;
    // Start is called before the first frame update
    void Start()
    {
    }

    public Vector2 moveDir 
    {
        get { return _direction; }    
    
    }



    public void Follow(GameObject target)
    {
        float otherDistance = target.transform.position.x - gameObject.transform.position.x;


        if (otherDistance > 0)
            _direction = Vector2.right;
        else if (otherDistance < 0)
            _direction = Vector2.left;
    }

    public void Flee(GameObject target)
    {
        float otherDistance = target.transform.position.x - gameObject.transform.position.x;


        if (otherDistance < 0)
            _direction = Vector2.right;
        else if (otherDistance > 0)
            _direction = Vector2.left;
    }
}
