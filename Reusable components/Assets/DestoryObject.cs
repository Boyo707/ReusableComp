using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObject : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    public delegate void DestroyAction();
    //public DestroyAction action = delegate { };
    public event DestroyAction action = delegate { };

    private void OnDestroy()
    {
        
        if (_particlePrefab != null)
        {
            Vector2 lastPos = gameObject.transform.position;
            Instantiate(_particlePrefab, lastPos, Quaternion.identity);
        }
        action();
    }

}
