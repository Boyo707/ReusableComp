using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherController : MonoBehaviour, IEnemy
{
    [SerializeField] private int _points;
    [SerializeField] GameObject _UIScoreCanvas;
    [SerializeField] private Transform _targetTransform; 
    private AttackProjectile _attack;
    private Health _health;
    private SpriteRenderer _spR;

    private float angle;
    //later de knockback van de archer wat lager zetten in de inspector.
    // Start is called before the first frame update

    public Transform TargetTransform
    {
        get { return _targetTransform; }
    }

    public Transform ParentTransform
    {
        get { return gameObject.transform; }
    }

    void Start()
    {
        _attack = GetComponent<AttackProjectile>();
        _health = GetComponent<Health>();
        _spR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("This object cords are " + gameObject.transform.position + " target cords are " + _targetTransform.transform.position);
        /*if(!_health.knocked)
            _attack.Attack(true, _spR.flipX);*/
    }

    private void OnDestroy()
    {
        _UIScoreCanvas.GetComponent<UIScore>().AddPoints(_points);
    }
}
