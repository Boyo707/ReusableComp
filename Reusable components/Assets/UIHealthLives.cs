using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIHealthLives : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = $"Health = {_playerHealth.HealthInt}\nLives = {_playerHealth.LivesInt}";
    }
}
