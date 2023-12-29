using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public static Action PlayerReachedExit;
    public static Action PlayerLanded;
    public static Action PlayerGotOffGrounbd;
    public static Action<Coin> PlayerPickedUpCoin;

    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PhisicalPlatform phisicalPlatform))
            PlayerLanded?.Invoke();
        else if (collision.TryGetComponent(out VictoryPoint victoryPoint))
            PlayerReachedExit?.Invoke();
        else if (collision.TryGetComponent(out Coin coin))
            PlayerPickedUpCoin?.Invoke(coin);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PhisicalPlatform phisicalPlatform))
            PlayerGotOffGrounbd?.Invoke();
    }
}
