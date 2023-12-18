using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    [SerializeField, Min(1)] private int _movingSpeed;

    private Animator _animator;

    private int _pushForce = 200;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("isRunning");
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

            player.GetRigidbody2D().AddForce(pushDirection * _pushForce);
        }
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * _movingSpeed, 0, 0);
    }
}

