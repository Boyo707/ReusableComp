using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum pickupTypes
{
    Health,
    Projectile,
    Live,
    PowerUp
}


public class PickUp : MonoBehaviour
{
    [SerializeField] private int _addAmount;
    [SerializeField] private pickupTypes _pickupType;

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
            if (_pickupType == pickupTypes.Health)
                collision.GetComponent<Health>().HealthInt += _addAmount;
            else if (_pickupType == pickupTypes.Projectile)
                collision.GetComponent<AttackProjectile>().projectileAmounts += _addAmount;
            else if (_pickupType == pickupTypes.Live)
                collision.GetComponent<Health>().LivesInt += _addAmount;
            else if (_pickupType == pickupTypes.PowerUp)
                Debug.Log("POWERUP");
            Destroy(gameObject);
        }
    }
}
