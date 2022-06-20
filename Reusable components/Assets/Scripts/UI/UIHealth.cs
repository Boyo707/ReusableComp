using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    private Slider _slider;
    private IHealth _targetHealth;
    // Start is called before the first frame update
    void Start()
    {
        _targetHealth = _target.GetComponent<IHealth>();
        _slider = GetComponent<Slider>();
        _slider.maxValue = _targetHealth.HealthInt;
        _slider.value = _slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _targetHealth.HealthInt;
    }
}
