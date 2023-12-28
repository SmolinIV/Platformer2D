using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour, IDamagable
{
    private EnemyActions _actions;
    private EnemyRay _ray;
    private Attack _attack;

    private int _health;

    public bool _isPlayerFind;

    private void Start()
    {
        _health = 100;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PatrollingBarrier barrier))
            _actions.TurnAround();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
            _actions.Push(player);
    }

    private void Update()
    {
        if (_ray.TryFindPlayer() && !_isPlayerFind)
        {
            _isPlayerFind = true;
            _actions.SpeedUp();
        }
        else if (!_ray.TryFindPlayer())
        {
            _isPlayerFind = false;
        }

        _actions.Move();     
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die() => Destroy(gameObject);
}

