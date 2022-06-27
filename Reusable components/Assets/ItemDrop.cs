using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private bool _dropsCurrentProjectile;
    [SerializeField] private GameObject _playerRefrence = null;

    private ProfectileStorage _projectileStorage;
    private PickUpStorage _pickUpStorage;
    // Start is called before the first frame update
    void Start()
    {
        if (_items.Count == 0)
            Debug.LogError("No item has been added to 'ItemDrop' script in " + gameObject.name);
        if (_dropsCurrentProjectile)
        {
            if (_playerRefrence == null)
                Debug.LogError("No player refrence has been set in 'ItemDrop' scipt in " + gameObject.name);
            else
            {
                _projectileStorage = FindObjectOfType<ProfectileStorage>();
                _pickUpStorage = FindObjectOfType<PickUpStorage>();
            }
        }
    }

    public void DropItem()
    {
        if (_playerRefrence)
        {
            //manier vinden hoe ik dit in een case kan zetten.
            if(_playerRefrence.GetComponent<AttackProjectile>().currentProjectile == _projectileStorage.Knife)
                _items.Add(_pickUpStorage.Knife);
        }
        if(_items.Count != 0)
            Instantiate(_items[Random.Range(0, _items.Count)], transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        DropItem();
    }
}
