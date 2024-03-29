using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private Mover _mover;
    private InputHandler _input;
    private ShurikenThrower _attack;
    private Wallet _wallet;
    private Health _health;
    private CoinTaker _coinTaker;
    private CollisionHandler _collisionHandler;
    private SkillContainer _skills;

    private Vector2 _startPosition;
    private Vector2 _startScale;

    public Rigidbody2D Rigidbody2D { get; private set; }
    public bool IsOnGround { get; private set; }
    public bool IsDied { get; private set; }
    public bool IsWin { get; private set; }

    public void OnEnable()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _collisionHandler.Landed += Land;
        _collisionHandler.Won += Win;
        _collisionHandler.CoinTaken += TakeCoin;
        _collisionHandler.GetOffGround += GetOffGround;
    }

    public void OnDisable()
    {
        _collisionHandler.Landed -= Land;
        _collisionHandler.Won -= Win;
        _collisionHandler.CoinTaken -= TakeCoin;
        _collisionHandler.GetOffGround -= GetOffGround;
    }

    private void Start()
    {
        _mover = GetComponent<Mover>();
        _input = GetComponent<InputHandler>();
        _attack = GetComponent<ShurikenThrower>();
        _wallet = GetComponent<Wallet>();
        _health = GetComponent<Health>();
        _coinTaker = GetComponent<CoinTaker>();
        _skills = GetComponent<SkillContainer>();
        

        Rigidbody2D = GetComponent<Rigidbody2D>();

        _startPosition = transform.position;
        _startScale = transform.localScale;

        IsOnGround = true;
        IsDied = false;
        IsWin = false;
    }

    private void Update()
    {
        if (_health.Current <= 0)
        {
            if (IsDied)
            {
                if (!_mover.IsReviving)
                {
                    _health.Recover();
                    IsDied = false;
                }

                return;
            }
            else
            {
                Die();
                _skills.DeactiveAllSkills();

                return;
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

        foreach (Skill skill in _skills.GetAllSkills())
        {
            if (_input.IsCurrentKeyPressed(skill.GetActivationKeyCode()))
                skill.Activate();
        }
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

    public void TakeDamage(float damage) => _health.TakeDamage(damage);

    public void TakeHeal(float healingPoint) => _health.TakeHeal(healingPoint);
}