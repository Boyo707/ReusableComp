using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UILives : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    private Health _health;
    private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _health = _target.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        int firstNumber = (_health.LivesInt / 10) % 10;

        if (firstNumber != 0)
        {
            int secondNumber = _health.LivesInt % 10;
            _text.text = $"<sprite=10> <sprite={firstNumber}> <sprite={secondNumber}>";
        }
        else
            _text.text = $"<sprite=10> <sprite={_health.LivesInt}>";

    }
}
