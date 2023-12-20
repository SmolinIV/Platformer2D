using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTaker : MonoBehaviour
{
    public static Action CoinPickUp;

    public void TakeCoin(Coin coin)
    {
        Destroy(coin.gameObject);
        CoinPickUp?.Invoke();
    }
}