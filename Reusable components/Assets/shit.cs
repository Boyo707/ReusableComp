using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shit : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Vector3.Angle(gameObject.transform.position, _targetTransform.position);
        Debug.Log(angle);
    }
}
