using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpearThrowerController : MonoBehaviour, IEnemy
{
    [SerializeField] private int _points;
    [SerializeField] private GameObject _UIScoreCanvas;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _maxDistance;

    private WalkMovement _movement;
    private EnemyFollowing _follow;
    private AttackProjectile _attack;
    private Health _health;
    private SpriteRenderer _spR;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _attack = GetComponent<AttackProjectile>();
        _health = GetComponent<Health>();
        _spR = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _movement = GetComponent<WalkMovement>();
        _follow = GetComponent<EnemyFollowing>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_health.knocked)
        {
            float distance = _target.transform.position.x - gameObject.transform.position.x;


            if (distance <= _maxDistance && distance > 0)
            {
                _movement.MoveInput(Vector2.zero);
                _attack.Attack(true, _spR.flipX);
            }
            else if (distance < 0 && distance >= -_maxDistance)
            {
                _movement.MoveInput(Vector2.zero);
                _attack.Attack(true, _spR.flipX);
            }
            else
            {
                _follow.Follow(_target);
                _movement.MoveInput(_follow.moveDir);
            }


        }
    }
    private void OnDestroy()
    {
        _UIScoreCanvas.GetComponent<UIScore>().AddPoints(_points);
    }
}
