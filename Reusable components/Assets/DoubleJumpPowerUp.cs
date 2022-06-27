using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IPlayer>() != null)
        {
            
            Destroy(collision.GetComponent<Jump>());
            collision.gameObject.AddComponent<DoubleJump>();

            Destroy(gameObject);
        }
    }
}
