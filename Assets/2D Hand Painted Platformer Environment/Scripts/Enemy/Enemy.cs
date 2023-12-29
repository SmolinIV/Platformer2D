using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour, IDamagable
{
    private EnemyMover _actionImplementor;
    private EnemyRay _ray;

    private int _health;

    private bool _isPlayerFind;

    private void Start()
    {
        _actionImplementor = GetComponent<EnemyMover>();
        _ray = GetComponent<EnemyRay>();

        _health = 100;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PatrollingBarrier barrier))
        {
            if (_isPlayerFind)
                _actionImplementor.NeedStop();
            else
                _actionImplementor.TurnAround();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
            _actionImplementor.Push(player);
    }

    private void FixedUpdate()
    {
        if (_ray.TryFindPlayer())
        {
            if (!_isPlayerFind)
            {
                _isPlayerFind = true;
                _actionImplementor.Pounce();
            }

            return;
        }
        else
        {
            if (_isPlayerFind)
            {
                _isPlayerFind = false;
                _actionImplementor.ReturnToPatrol();
            }
        }

        _actionImplementor.Move();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die() => Destroy(gameObject);
}

