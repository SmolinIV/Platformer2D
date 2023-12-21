using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTaker : MonoBehaviour
{
    public static Action CoinPickUp;

    private void OnEnable() => CollisionHandler.PlayerPickedUpCoin += TakeCoin;

    private void OnDisable() => CollisionHandler.PlayerPickedUpCoin -= TakeCoin;

    public void TakeCoin(Coin coin)
    {
        if (coin != null)
        {
            Destroy(coin.gameObject);
            CoinPickUp?.Invoke();
        }
    }
}