using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum pickupTypes
{
    Health,
    Projectile,
    Live,
    PowerUp
}

public enum projectileType
{
    Knife,
    Sword
}


public class PickUp : MonoBehaviour
{
    [SerializeField] private int _addAmount;
    [SerializeField] private pickupTypes _pickupType;
    [SerializeField] private projectileType _projectileType;

    private GameObject playerIg;

    private GameObject fuckyou;

    private ProfectileStorage _projectileStorage;

    private bool inside;

    public int Amount 
    {
        set { _addAmount = value; }
    }

    public pickupTypes PickUpType
    {
        set { _pickupType = value; }
    }

    public projectileType ProjectileType
    {
        set { _projectileType = value; }
    }


    void Start()
    {
        _projectileStorage = FindObjectOfType<ProfectileStorage>();

        switch (_projectileType)
        {
            case projectileType.Sword:
                fuckyou = _projectileStorage.Sword;
                break;

            case projectileType.Knife:
                fuckyou = _projectileStorage.Knife;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inside == true)
        {
            playerIg.transform.GetChild(3).gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.P))
            {
                //playerIg.GetComponent
                playerIg.transform.GetChild(3).gameObject.SetActive(false);
                switch (_projectileType)
                {
                    case projectileType.Sword:
                        playerIg.GetComponent<AttackProjectile>().currentProjectile = _projectileStorage.Sword;
                        break;

                    case projectileType.Knife:
                        playerIg.GetComponent<AttackProjectile>().currentProjectile = _projectileStorage.Knife;
                        break;
                }
                
                
                playerIg.GetComponent<AttackProjectile>().projectileAmounts = _addAmount;

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IPlayer>() != null)
        {
            if (_pickupType == pickupTypes.Health)
            {
                collision.GetComponent<Health>().HealthInt += _addAmount;
                Destroy(gameObject);
            }
                
            else if (_pickupType == pickupTypes.Projectile)
            {

                //check if pickup is the same as current projectile.
                if (fuckyou == collision.GetComponent<AttackProjectile>().currentProjectile)
                {
                    collision.GetComponent<AttackProjectile>().projectileAmounts += _addAmount;
                    Destroy(gameObject);
                }
                else
                {
                    playerIg = collision.gameObject;
                    inside = true;
                }
                    

            }
            else if (_pickupType == pickupTypes.Live)
            {
                collision.GetComponent<Health>().LivesInt += _addAmount;
                Destroy(gameObject);
            }
            
            else if (_pickupType == pickupTypes.PowerUp)
                Debug.Log("POWERUP");
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inside = false;
        if(collision.gameObject.transform.childCount > 2 && !inside)
            collision.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        
    }

}
