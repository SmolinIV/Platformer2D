using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public delegate void LandHandler();
    public delegate void JumpHendler();
    public delegate void VictoryHandler();
    public delegate void CoinTakingHandler(Coin coin);

    public event LandHandler Landed;
    public event JumpHendler GetOffGround;
    public event VictoryHandler Won;
    public event CoinTakingHandler CoinTaken;

    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

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
