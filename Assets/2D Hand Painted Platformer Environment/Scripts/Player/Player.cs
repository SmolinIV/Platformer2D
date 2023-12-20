using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private CoinTaker _taker;

    private Vector2 _startPosition;
    private Vector2 _startScale;

    private int _coinsNumber;

    public Rigidbody2D Rigidbody2D { get; private set; }
    public bool IsOnGround { get; private set; }
    public bool IsDied { get; private set; }
    public bool IsWin { get; private set; }

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
        _taker = GetComponent<CoinTaker>();
        Rigidbody2D = GetComponent<Rigidbody2D>();

        _startPosition = transform.position;
        _startScale = transform.localScale;

        IsOnGround = true;
        IsDied = false;
        IsWin = false;
    }

    public void Die()
    {
        IsDied = true;
        _mover.RespawnToStart(_startPosition, _startScale);
    }

    public void Ressurect() => IsDied = false;

    public void Win()
    {
        IsWin = true;
        Idle();
        _mover.StartWinnerJumping();
    }

    public void GetOffGround() => IsOnGround = false;

    public void PutDownOnGround()
    {
        IsOnGround = true;
        _mover.Land();
    }

    public void Run(int directionDegree) => _mover.Run(directionDegree);

    public void Idle() => _mover.Idle();

    public void Fall() => _mover.Fall();

    public void Jump()
    { 
        if (IsOnGround)
            _mover.Jump();    
    }

    public void VictorioslyJump() => _mover.StartWinnerJumping();

    public void TakeCoin(Coin coin)
    {
        if (coin != null)
        {
            ++_coinsNumber;
            _taker.TakeCoin(coin);
        }
    }
}