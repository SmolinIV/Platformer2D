using System.Collections;
using UnityEngine;
using System;

public class EnemyActionsImplementor : MonoBehaviour
{
    public static Action KilledPlayer;

    private readonly string EnemyRunningPermit = "isRunning";

    [SerializeField, Min(1)] private int _movingSpeed;
    [SerializeField, Min(1)] private int _speedUpNumber;
    private Attack _attack;
    private Animator _animator;

    private Coroutine _rushingCoroutine;
    private Coroutine _attackingCoroutine;

    private bool _isNeedToStop;

    public void OnDisable()
    {
        if (_rushingCoroutine != null)
            StopCoroutine(_rushingCoroutine);

        if (_attackingCoroutine != null)
            StopCoroutine(_attackingCoroutine);
    }

    private void Start()
    {
        _attack = GetComponent<Attack>();
        _animator = GetComponent<Animator>();
        _animator.SetBool(EnemyRunningPermit, true);
    }

    public void Move()
    {
        transform.Translate(Time.deltaTime * _movingSpeed, 0, 0);
    }

    public void TurnAround()
    {
        int directionDegree = 180;

        if (transform.rotation.eulerAngles.y == directionDegree)
            directionDegree = 0;

        transform.rotation = new Quaternion(0, directionDegree, 0, 0);

    }

    public void Push(Player player)
    {
        int _pushForce = 300;

        Vector2 pushDirection = (player.transform.position - transform.position).normalized;
        player.Rigidbody2D.AddForce(pushDirection * _pushForce);

        KilledPlayer?.Invoke();
    }

    public void Pounce()
    {
        SpeedUp();
        _rushingCoroutine = StartCoroutine(Rush());
        _attackingCoroutine = StartCoroutine(Attack());
    }

    public void ReturnToPatrol()
    {
        StopCoroutine(_rushingCoroutine);
        StopCoroutine(_attackingCoroutine);

        SlowDown();

        if (_isNeedToStop)
        {
            TurnAround();
            _animator.SetBool(EnemyRunningPermit, true);
            _isNeedToStop = false;
        }
    }

    public void SpeedUp() => _movingSpeed += _speedUpNumber;

    public void SlowDown() => _movingSpeed -= _speedUpNumber;

    public void NeedStop() => _isNeedToStop = true;

    public IEnumerator Rush()
    {
        while (!_isNeedToStop)
        {
            Move();
            yield return null;
        }

        _animator.SetBool(EnemyRunningPermit, false);
    }

    public IEnumerator Attack()
    {
        while (true)
        {
            _attack.ThrowShuriken();
            yield return null;
        }
    }
}
