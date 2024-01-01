using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour, IDamagable
{
    private EnemyMover _mover;
    private EnemyScanner _scanner;
    private HealthContol _healthContol;
    private EnemyAttacker _attacker;

    private float _maxTrackingTime;
    private float _currentTrackingTime;

    private bool _isPlayerFind;

    private void Start()
    {
        _mover = GetComponent<EnemyMover>();
        _scanner = GetComponent<EnemyScanner>();
        _healthContol = GetComponent<HealthContol>();
        _attacker =  GetComponent<EnemyAttacker>();

        _maxTrackingTime = 2f;
        _currentTrackingTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
            _mover.Push(player);
    }

    private void Update()
    {
        if (_healthContol.CurrentHealth <= 0)
            Die();

        if (_isPlayerFind)
        {
            _currentTrackingTime += Time.deltaTime;

            if (_currentTrackingTime >= _maxTrackingTime)
            {
                _currentTrackingTime = 0;

                if (!_scanner.TryFindPlayer())
                { 
                    _isPlayerFind = false;
                    _mover.ReturnToPatrol();
                    _attacker.StopAttack();
                }
            }
        }
        else if (_scanner.TryFindPlayer())
        {
            _isPlayerFind = true;
            _mover.Pounce();
            _attacker.Attack();
        }
        else
        {
            _mover.Patrolling();
        }
    }

    public void TakeDamage(int damage) => _healthContol.TakeDamage(damage);

    private void Die() => Destroy(gameObject);
}