using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action Landed;
    public event Action GetOffGround;
    public event Action Won;
    public event Action<Coin> CoinTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PhisicalPlatform phisicalPlatform))
            Landed?.Invoke();
        else if (collision.TryGetComponent(out VictoryPoint victoryPoint))
            Won?.Invoke();
        else if (collision.TryGetComponent(out Coin coin))
            CoinTaken?.Invoke(coin);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PhisicalPlatform phisicalPlatform))
            GetOffGround?.Invoke();
    }
}
