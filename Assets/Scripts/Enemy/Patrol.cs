using System.Collections;
using UnityEngine;
using System;

public class Patrol : MonoBehaviour
{
    private readonly string EnemyRunningPermit = "isRunning";

    [SerializeField, Min(1)] private int _movingSpeed;
    [SerializeField, Min(1)] private int _speedUpNumber;
    [SerializeField] private Transform _leftPatrolPoint;
    [SerializeField] private Transform _rightPatrolPoint;

    private Animator _animator;
    private Coroutine _rushingCoroutine;

    private Vector3 _leftPatrolPointPosition;
    private Vector3 _rightPatrolPointPosition;

    private void Awake()
    {
        _leftPatrolPointPosition = _leftPatrolPoint.position;
        _rightPatrolPointPosition = _rightPatrolPoint.position;

        _leftPatrolPoint.gameObject.SetActive(false);
        _rightPatrolPoint.gameObject.SetActive(false);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(EnemyRunningPermit, true);
    }

    private void OnDisable()
    {
        if (_rushingCoroutine != null)
            StopCoroutine(_rushingCoroutine);
    }

    public void Patrolling()
    {
        int rightMovingDegree = 0;

        if (transform.rotation.y == rightMovingDegree && transform.position.x >= _rightPatrolPointPosition.x)
        {
            TurnAround();
        }
        else
        {
            if (transform.position.x <= _leftPatrolPointPosition.x)
                TurnAround();
        }

        Move();
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

        player.TakeDamage(Int32.MaxValue);
    }

    public void Pounce()
    {
        SpeedUp();
        _rushingCoroutine = StartCoroutine(Rush());
        
    }

    public void ReturnToPatrol()
    {
        if (_rushingCoroutine != null)
            StopCoroutine(_rushingCoroutine);

        SlowDown();

        _animator.SetBool(EnemyRunningPermit, true);
    }

    public IEnumerator Rush()
    {
        while (transform.position.x >= _leftPatrolPointPosition.x && transform.position.x <= _rightPatrolPointPosition.x)
        {
            Move();
            yield return null;
        }

        _animator.SetBool(EnemyRunningPermit, false);

        yield break;
    }

    private void SpeedUp() => _movingSpeed += _speedUpNumber;

    private void SlowDown() => _movingSpeed -= _speedUpNumber;
}
