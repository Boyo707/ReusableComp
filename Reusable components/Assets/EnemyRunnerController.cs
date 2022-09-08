using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunnerController : MonoBehaviour, IEnemy
{
    [SerializeField] private int _points;
    [SerializeField] GameObject _UIScoreCanvas;
    [SerializeField] GameObject _target;
    [SerializeField] private ParticleSystem _sweatParticles;

    private HorizontalMovement movements;
    private EnemyFollowing _follow;
    private AttackProjectile _attack;
    private SpriteRenderer _spR;
    private Jump _jump;
    private bool _flipped;
    private Vector2 _direction;
    private Health _health;

    private float _time;

    private bool _once;
    
    // Start is called before the first frame update
    void Start()
    {
        _time = 1.2f;
        _health = GetComponent<Health>();
        movements = GetComponent<HorizontalMovement>();
        _follow = GetComponent<EnemyFollowing>();
        _attack = GetComponent<AttackProjectile>();
        _spR = GetComponent<SpriteRenderer>();
        _jump = GetComponent<Jump>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!_health.knocked)
        {
            float distance = _target.transform.position.x - gameObject.transform.position.x;

            //_jump.JumpInput(true);

            if (_attack.projectileAmounts != 0)
            {
                if (distance <= 4.5f && distance > 0)
                    _attack.Attack(true, _spR.flipX);
                else if(distance < 0 && distance >= -4.5f)
                    _attack.Attack(true, _spR.flipX);
                else
                {
                    _follow.Follow(_target);
                    //movements.MoveInput(_follow.moveDir);
                }

            }
            else
            {
                
                if (_time <= 0)
                {
                    _sweatParticles.Play();
                    _follow.Flee(_target);
                    //movements.MoveInput(_follow.moveDir, true);
                }
                else if (_time > 0)
                {
                    _time -= Time.deltaTime;
                }

            }
            
                  
        }
    }

    private void OnDestroy()
    {
        _UIScoreCanvas.GetComponent<UIScore>().AddPoints(_points);
    }
}
