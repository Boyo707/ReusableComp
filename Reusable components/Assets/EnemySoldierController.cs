using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldierController : MonoBehaviour, IEnemy
{
    [SerializeField] private int _points;
    [SerializeField] GameObject _UIScoreCanvas;
    [SerializeField] GameObject _target;

    private MovementState _movements;
    private EnemyFollowing _follow;
    private AttackMellee _attack;
    private SpriteRenderer _spR;
    private JumpState _jump;
    private bool _flipped;
    private Vector2 _direction;
    private Health _health;

    private float _time;

    private bool _once;
    // Start is called before the first frame update
    void Start()
    {
        _spR = GetComponent<SpriteRenderer>();
        _movements = GetComponent<MovementState>();
        _jump = GetComponent<JumpState>();
        _health = GetComponent<Health>();
        _attack = GetComponent<AttackMellee>();
        _follow = GetComponent<EnemyFollowing>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!_health.knocked)
        {
            //_jump.JumpInput(true);


            _attack.Attack(true, _spR.flipX);

            _follow.Follow(_target);

            //_movements.MoveInput(_follow.moveDir, true);


        }*/
    }
    private void OnDestroy()
    {
        _UIScoreCanvas.GetComponent<UIScore>().AddPoints(_points);
    }
}
