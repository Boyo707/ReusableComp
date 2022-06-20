using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    private AttackProjectile _attack;
    private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _attack = _target.GetComponent<AttackProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
        int firstNumber = (_attack.projectileAmounts / 10) % 10;
        
        if (firstNumber != 0)
        {
            int secondNumber = _attack.projectileAmounts % 10;
            _text.text =  $"<sprite=10> <sprite={firstNumber}> <sprite={secondNumber}>";
        }
        else
            _text.text = $"<sprite=10> <sprite={_attack.projectileAmounts}>";

        
    }
}
