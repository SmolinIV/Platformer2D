using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private readonly string EnemyRunningPermit = "isRunning";
    private readonly string EnemyJumpingPermit = "isJumping";

    [SerializeField, Min(1)] private int _movingSpeed;
    [SerializeField, Min(1)] private int _forceJump;

    private PlayerCondition _condition;

    private Coroutine _victoriouslyJumping;
    private Rigidbody2D _rigidbody2d;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _condition = GetComponent<PlayerCondition>();
    }

    private void OnDestroy()
    {
        if (_victoriouslyJumping != null)
            StopCoroutine(_victoriouslyJumping);
    }

    private void Update()
    {
        if (_condition.IsWin || _condition.IsDied)
            return;

        int rightDirectionDegree = 0;
        int lefDirectionDegree = 180;

        if (Input.GetKey(KeyCode.RightArrow))
            Run(rightDirectionDegree);
        else if (Input.GetKey(KeyCode.LeftArrow))
            Run(lefDirectionDegree);
        else
            _animator.SetBool(EnemyRunningPermit, false);

        if (Input.GetKeyDown(KeyCode.UpArrow) && _condition.IsOnGround)
            Jump();
    }

    public void StartWinnerJumping()
    {
        _victoriouslyJumping = StartCoroutine(JumpVictoriously());
    }

    private void Run(int directionDegree)
    {
        _animator.SetBool(EnemyRunningPermit, true);

        transform.rotation = new Quaternion(0, directionDegree, 0, 0);
        transform.Translate(Time.deltaTime * _movingSpeed, 0, 0);
    }

    private void Jump()
    {
        _animator.SetBool(EnemyJumpingPermit, true);
        _rigidbody2d.AddForce(new Vector2(0, _forceJump));
    }

    private IEnumerator JumpVictoriously()
    {
        int forceJump = 150;
        float jumpingDelay = 1.0f;

        while (_condition.IsWin)
        {
            _animator.SetBool(EnemyJumpingPermit, true);
            _rigidbody2d.AddForce(new Vector2(0, forceJump));

            yield return new WaitForSeconds(jumpingDelay);
        }
    }
}
