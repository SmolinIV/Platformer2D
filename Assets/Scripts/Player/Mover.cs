using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private readonly string EnemyRunningPermit = "isRunning";
    private readonly string EnemyJumpingPermit = "isJumping";

    [SerializeField, Min(1)] private int _movingSpeed;
    [SerializeField, Min(1)] private int _forceJump;

    private Coroutine _victoriouslyJumping;
    private Rigidbody2D _rigidbody2d;
    private Animator _animator;

    public bool IsReviving { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();

        IsReviving = true;
    }

    private void OnDestroy()
    {
        if (_victoriouslyJumping != null)
            StopCoroutine(_victoriouslyJumping);
    }


    public void Run(int directionDegree)
    {
        _animator.SetBool(EnemyRunningPermit, true);

        transform.rotation = new Quaternion(0, directionDegree, 0, 0);
        transform.Translate(Time.deltaTime * _movingSpeed, 0, 0);
    }

    public void Idle() => _animator.SetBool(EnemyRunningPermit, false);

    public void Fall() => _animator.SetBool(EnemyJumpingPermit, true);

    public void Land() => _animator.SetBool(EnemyJumpingPermit, false);

    public void StartWinnerJumping(bool isPlayerWin) => _victoriouslyJumping = StartCoroutine(JumpVictoriously(isPlayerWin));

    public void Jump()
    {
        _animator.SetBool(EnemyJumpingPermit, true);
        _rigidbody2d.AddForce(new Vector2(0, _forceJump));
    }

    public void RespawnToStart(Vector2 startPosition, Vector2 startScale)
    {
        IsReviving = true;
        StartCoroutine(Respawn(startPosition, startScale));
    }

    private IEnumerator JumpVictoriously(bool isPlayerWin)
    {
        int forceJump = 200;
        float jumpingDelay = 1.0f;

        while (isPlayerWin)
        {
            _animator.SetBool(EnemyJumpingPermit, true);
            _rigidbody2d.AddForce(new Vector2(0, forceJump));

            yield return new WaitForSeconds(jumpingDelay);
        }
    }

    private IEnumerator Respawn(Vector2 startPosition, Vector2 startScale)
    {
        float respawnDelay = 0.5f;
        int decreasingSpeed = 2;

        while (transform.localScale.y > 0)
        {
            transform.localScale = new Vector2(transform.localScale.x - Time.deltaTime * decreasingSpeed, transform.localScale.y - Time.deltaTime * decreasingSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(respawnDelay);

        Idle();

        transform.position = startPosition;
        transform.localScale = startScale;
        IsReviving = false;

        yield break;
    }
}
