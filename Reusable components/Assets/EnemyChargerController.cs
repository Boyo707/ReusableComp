using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargerController : MonoBehaviour, IEnemy
{
    [SerializeField] private int _points;
    [SerializeField] GameObject _UIScoreCanvas;
    [SerializeField] GameObject _target;


    private EnemyFollowing _follow;
    private AttackMellee _attack;
    private SpriteRenderer _spR;

    private Health _health;
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>();
        _follow = GetComponent<EnemyFollowing>();
        _attack = GetComponent<AttackMellee>();
        _spR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_health.knocked)
        {
            //_jump.JumpInput(true);


            _attack.Attack(true, _spR.flipX);

            _follow.Follow(_target);

            //_movement.MoveInput(_follow.moveDir, true);
        }
    }

    private void OnDestroy()
    {
        _UIScoreCanvas.GetComponent<UIScore>().AddPoints(_points);
    }
}
