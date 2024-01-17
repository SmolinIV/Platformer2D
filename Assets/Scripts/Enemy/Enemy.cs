using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour, IDamagable
{
    private Patrol _mover;
    private Scanner _scanner;
    private Health _health;
    private Attacker _attacker;

    private float _maxTrackingTime;
    private float _currentTrackingTime;

    private bool _isPlayerFind;

    private void Start()
    {
        _mover = GetComponent<Patrol>();
        _scanner = GetComponent<Scanner>();
        _health = GetComponent<Health>();
        _attacker =  GetComponent<Attacker>();

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
        if (_health.Current <= 0)
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

    public void TakeDamage(float damage) => _health.TakeDamage(damage);

    private void Die() => Destroy(gameObject);

    public void TakeHeal(float healingPoint) => _health.TakeHeal(healingPoint);
}