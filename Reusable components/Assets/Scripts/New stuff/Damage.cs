using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Damage : MonoBehaviour
{

    [Header("Damage values")]
    [SerializeField] private LayerMask _damagableLayers;
    [SerializeField] private float _damageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (_damagableLayers == (_damagableLayers | (1 << collision.gameObject.layer)))
        {
            if (collision.GetComponent<Health>())
            {
                collision.GetComponent<Health>().TakeDamage(_damageAmount);
            }
        }
    }

    

}
