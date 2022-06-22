using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimParams : MonoBehaviour
{

    [SerializeField] private Animator healthBarAnimator;
    [SerializeField] private GameObject _player;
    private Health _health;
    // Start is called before the first frame update
    void Start()
    {
        _health = _player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {



        healthBarAnimator.SetBool("Hurt", _health.knocked);
    }
}
