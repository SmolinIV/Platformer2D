using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCondition : MonoBehaviour
{
    private readonly string EnemyRunningPermit = "isRunning";
    private readonly string EnemyJumpingPermit = "isJumping";

    private PlayerMover _playerMover;
    private Animator _animator;
    private Vector2 _startPosition;
    private Vector2 _startScale;

    public Rigidbody2D Rigidbody2D { get; private set; }
    public bool IsOnGround { get; private set; }
    public bool IsDied { get; private set; }
    public bool IsWin { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();

        Rigidbody2D = GetComponent<Rigidbody2D>();

        _startPosition = transform.position;
        _startScale = transform.localScale;

        IsOnGround = true;
        IsDied = false;
        IsWin = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out SurroundingThing surroundingThing))
        {
            IsOnGround = true;
            _animator.SetBool(EnemyJumpingPermit, false);
        }
        else if (collision.TryGetComponent(out VictoryPoint victoryPoint))
        {
            IsWin = true;

            ResetCondotion();
            _playerMover.StartWinnerJumping();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            IsDied = true;
            StartCoroutine(Respawn());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out SurroundingThing thing))
        {
            IsOnGround = false;
            _animator.SetBool(EnemyJumpingPermit, true);
        }
    }

    private void ResetCondotion()
    {
        transform.localScale = _startScale;

        _animator.SetBool(EnemyRunningPermit, false);
        _animator.SetBool(EnemyJumpingPermit, false);

        IsDied = false;
        IsOnGround = true;
    }

    private IEnumerator Respawn()
    {
        float respawnDelay = 0.5f;
        int decreasingSpeed = 1;

        while (transform.localScale.y > 0)
        {
            transform.localScale = new Vector2(transform.localScale.x - Time.deltaTime * decreasingSpeed, transform.localScale.y - Time.deltaTime * decreasingSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(respawnDelay);

        transform.position = _startPosition;
        ResetCondotion();

        yield break;
    }
}
