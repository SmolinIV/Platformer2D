using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private PlayerMover _mover;
    private InputHandler _input;
    private ShurikenThrower _attack;
    private Wallet _wallet;
    private Health _healthContol;
    private CoinTaker _coinTaker;

    private Vector2 _startPosition;
    private Vector2 _startScale;

    public Rigidbody2D Rigidbody2D { get; private set; }
    public bool IsOnGround { get; private set; }
    public bool IsDied { get; private set; }
    public bool IsWin { get; private set; }

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
        _input = GetComponent<InputHandler>();
        _attack = GetComponent<ShurikenThrower>();
        _wallet = GetComponent<Wallet>();
        _healthContol = GetComponent<Health>();
        _coinTaker = GetComponent<CoinTaker>();

        Rigidbody2D = GetComponent<Rigidbody2D>();

        _startPosition = transform.position;
        _startScale = transform.localScale;

        IsOnGround = true;
        IsDied = false;
        IsWin = false;
    }

    private void Update()
    {
        if (_healthContol.CurrentHealth <= 0)
        {
            if (IsDied)
            {
                IsDied = _mover.IsReviving;
                _healthContol.Recover();
                return;
            }
            else
            {
                Die();
            }
        }

        if (IsWin)
            return;

        int rightDirectionDegree = 0;
        int leftDirectionDegree = 180;

        if (_input.IsRightArrowPressed())
            _mover.Run(rightDirectionDegree);
        else if (_input.IsLeftArrowPressed())
            _mover.Run(leftDirectionDegree);
        else
            _mover.Idle();

        if (_input.IsUpArrowPressed() && IsOnGround)
            Jump();

        if (_input.IsSpacePressed())
            _attack.ThrowShuriken();
    }

    public void Die()
    {
        IsDied = true;
        _mover.RespawnToStart(_startPosition, _startScale);
    }

    public void Win()
    {
        IsWin = true;
        _mover.Idle();
        _mover.StartWinnerJumping(IsWin);
    }

    public void GetOffGround() => IsOnGround = false;

    public void Land()
    {
        IsOnGround = true;
        _mover.Land();
    }

    public void Fall()
    {
        GetOffGround();
        _mover.Fall();
    }

    public void Jump() => _mover.Jump();
    
    public void TakeCoin(Coin coin) => _coinTaker.TakeCoin(coin);

    public void TakeDamage(int damage) => _healthContol.TakeDamage(damage);

}