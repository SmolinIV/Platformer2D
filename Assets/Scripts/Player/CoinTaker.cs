using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTaker : MonoBehaviour
{
    public static Action CoinPickedUp;

    private Wallet _wallet;

    private void Start()
    {
        _wallet = GetComponent<Wallet>();
    }

    public void TakeCoin(Coin coin)
    {
        if (coin != null)
        {
            Destroy(coin.gameObject);
            _wallet.PutCoin();
            CoinPickedUp?.Invoke();
        }
    }
}