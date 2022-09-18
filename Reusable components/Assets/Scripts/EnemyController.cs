using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] private int _points;
    [SerializeField] GameObject _UIScoreCanvas;
    
    private IMovements movements;
    private EnemyFollowing _follow;
    private AttackProjectile _attack;
    private SpriteRenderer _spR;
    private bool _flipped;
    private Vector2 _direction;
    private Health _health;
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>();
        movements = GetComponent<IMovements>();
        _follow = GetComponent<EnemyFollowing>();
        _attack = GetComponent<AttackProjectile>();
        _spR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
            /*if (!_health.knocked)
            {
                bool attack = true;
                //movements.MoveInput(_follow.moveDir);
                _attack.Attack(attack, _spR.flipX);
            }*/
    }

    private void OnDestroy()
    {
        _UIScoreCanvas.GetComponent<UIScore>().AddPoints(_points);
    }
}
