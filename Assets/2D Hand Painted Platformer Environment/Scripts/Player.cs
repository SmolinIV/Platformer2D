using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField, Min(1)] private int _movingSpeed;
    [SerializeField, Min(1)] private int _forceJump;

    private Coroutine _victoriouslyJumping;
    private Rigidbody2D _rigidbody2d;
    private Animator _animator;
    private Vector2 _startPosition;
    private Vector2 _startScale;

    private bool _isOnGround;
    private bool _isDied;
    private bool _isWin;

    public int CoinsNumber { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();

        _startPosition = transform.position;
        _startScale = transform.localScale;

        CoinsNumber = 0;

        _isOnGround = true;
        _isDied = false;
        _isWin = false;
    }

    private void OnDestroy()
    {
        if (_victoriouslyJumping != null)
            StopCoroutine(_victoriouslyJumping);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thing thing))
        {
            _isOnGround = true;
            _animator.ResetTrigger("isJumping");
        }
        else if (collision.TryGetComponent(out Coin coin))
        {
            TakeCoin(coin);
        }
        else if (collision.TryGetComponent(out VictoryPoint victoryPoint))
        {
            _isWin = true;

            ResetCondotion();
            _victoriouslyJumping = StartCoroutine(JumpVictoriously());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thing thing))
        {
            _isOnGround = false;
            _animator.SetTrigger("isJumping");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            _isDied = true;
            StartCoroutine(Respawn());
        }
    }

    private void Update()
    {
        if (_isDied || _isWin)
            return;

        int rightDirectionDegree = 0;
        int lefDirectionDegree = 180;

        if (Input.GetKey(KeyCode.RightArrow))
            Run(rightDirectionDegree);
        else if (Input.GetKey(KeyCode.LeftArrow))
            Run(lefDirectionDegree);
        else
            _animator.ResetTrigger("isRunning");

        Jump();
    }

    public Rigidbody2D GetRigidbody2D() => _rigidbody2d;

    private void Run(int directionDegree)
    {
        _animator.SetTrigger("isRunning");

        transform.rotation = new Quaternion(0, directionDegree, 0, 0);
        transform.Translate(Time.deltaTime * _movingSpeed, 0, 0);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && _isOnGround)
        {
                _isOnGround = false;
                _animator.SetTrigger("isJumping");

                _rigidbody2d.AddForce(new Vector2(0, _forceJump));
        }
    }

    private void TakeCoin(Coin coin)
    {
        Destroy(coin.gameObject);
        ++CoinsNumber;
    }

    private void ResetCondotion()
    {
        transform.localScale = _startScale;

        _animator.ResetTrigger("isJumping");
        _animator.ResetTrigger("isRunning");

        _isDied = false;
        _isOnGround = true;
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

    private IEnumerator JumpVictoriously()
    {
        int forceJump = 150;
        float jumpingDelay = 1.0f;

        while (_isWin)
        {
                _animator.SetTrigger("isJumping");
                _rigidbody2d.AddForce(new Vector2(0, forceJump));

            yield return new WaitForSeconds(jumpingDelay);
        }
    }
}
