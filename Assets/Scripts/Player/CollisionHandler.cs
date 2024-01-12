using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public Action Landed;
    public Action GetOffGround;
    public Action Won;
    public Action<Coin> CoinTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PhisicalPlatform phisicalPlatform))
            Landed();
        else if (collision.TryGetComponent(out VictoryPoint victoryPoint))
            Won();
        else if (collision.TryGetComponent(out Coin coin))
            CoinTaken(coin);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PhisicalPlatform phisicalPlatform))
            GetOffGround();
    }
}
