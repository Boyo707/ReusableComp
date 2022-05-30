using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemy
{
    private IMovements movements;
    private EnemyFollowing _follow;
    private IAttack _attack;
    private SpriteRenderer _spR;
    private bool _flipped;
    private Vector2 _direction;
    // Start is called before the first frame update
    void Start()
    {
        movements = GetComponent<IMovements>();
        _follow = GetComponent<EnemyFollowing>();
        _attack = GetComponent<IAttack>();
        _spR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool attack = true;
        movements.MoveInput(_follow.moveDir);
        _attack.Attack(attack, _spR.flipX);
    }
}
