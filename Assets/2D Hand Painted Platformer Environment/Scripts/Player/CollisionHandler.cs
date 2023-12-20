using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PhisicalPlatform phisicalPlatform))
            _player.PutDownOnGround();
        else if (collision.TryGetComponent(out VictoryPoint victoryPoint))
            _player.Win();
        else if (collision.TryGetComponent(out Coin coin))
            _player.TakeCoin(coin);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
            _player.Die();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PhisicalPlatform phisicalPlatform))
        {
            _player.GetOffGround();
            _player.Fall();
        }
    }

}
