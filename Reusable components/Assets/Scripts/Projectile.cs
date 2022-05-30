using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{

    [SerializeField] private float _lifeTime;

    void Start()
    {
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_lifeTime != 0)
            Destroy(gameObject, _lifeTime);
    }


    


        //aparte scipt voor maken. 
        


}
