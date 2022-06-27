using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfectileStorage : MonoBehaviour
{
    [SerializeField] private GameObject _knife;
    [SerializeField] private GameObject _Sword;
    // Start is called before the first frame update

    public GameObject Knife
    {
        get { return _knife; }
    }

    public GameObject Sword
    {
        get { return _Sword; }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
