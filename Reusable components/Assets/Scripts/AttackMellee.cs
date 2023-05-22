using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMellee : MonoBehaviour, IAttack
{



    ///<Summary>
    /// Inspiratie video voor de attack scripts HEEL SIMPEL!?!??!?<see href="https://youtu.be/Xw8F57Ci_b8?t=393">HERE</see>
    /// Ook een functie maken dat als de colliders iets hitten dat ze uit gaan.
    /// Ook een functie voor de active duration.
    ///</Summary>

    [SerializeField] private GameObject _attack;

    [SerializeField] private LayerMask _layerMask;

    public void Attack(bool attack)
    {
        if (attack)
        {
            _attack.SetActive(true);
        }
        else
        {
            _attack.SetActive(false);
        }
    }

}
