using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpStorage : MonoBehaviour
{
    [SerializeField] private GameObject _health;
    [SerializeField] private GameObject _live;
    [SerializeField] private GameObject _knife;
    // Start is called before the first frame update


    public GameObject Knife
    {
        get { return _knife; }
    }

    public GameObject Health
    {
        get { return _health; }
    }
    public GameObject Live
    {
        get { return _live; }
    }
}
