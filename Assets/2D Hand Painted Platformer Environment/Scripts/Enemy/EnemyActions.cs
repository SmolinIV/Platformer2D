using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyActions : MonoBehaviour
{
    public static Action KilledPlayer;

    private readonly string EnemyRunningPermit = "isRunning";

    [SerializeField, Min(1)] private int _movingSpeed;

    private Animator _animator;

    private int _speedUpNumber = 1;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(EnemyRunningPermit, true);

    }
    public void Move()
    {
        transform.Translate(Time.deltaTime * _movingSpeed, 0, 0);
    }

    public void TurnAround()
    {
        int reversDegree = 180;

        transform.Rotate(0, reversDegree, 0);
    }

    public void Push(Player player)
    {
        int _pushForce = 300;

        Vector2 pushDirection = (player.transform.position - transform.position).normalized;
        player.Rigidbody2D.AddForce(pushDirection * _pushForce);

        KilledPlayer?.Invoke();
    }

    public void SpeedUp() => _movingSpeed += _speedUpNumber;

    public void SlowDown() => _movingSpeed -= _speedUpNumber;
}
