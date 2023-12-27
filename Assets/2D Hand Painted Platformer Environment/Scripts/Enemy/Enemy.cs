using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour, IDamagable
{
    public static Action KilledPlayer;

    private readonly string EnemyRunningPermit = "isRunning";

    [SerializeField, Min(1)] private int _movingSpeed;

    private Animator _animator;

    private int _pushForce = 300;
    private int _health;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(EnemyRunningPermit, true);

        _health = 100;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int reversDegree = 180;

        if (collision.TryGetComponent(out PatrollingBarrier barrier))
            transform.Rotate(0, reversDegree, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            Vector2 pushDirection = (player.transform.position - transform.position).normalized;

            player.Rigidbody2D.AddForce(pushDirection * _pushForce);

            KilledPlayer?.Invoke();
        }
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * _movingSpeed, 0, 0);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die() => Destroy(gameObject);
}

