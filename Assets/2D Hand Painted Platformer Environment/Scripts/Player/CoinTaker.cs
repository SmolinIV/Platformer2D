using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinTaker : MonoBehaviour
{
    public UnityEvent CoinPickUp;

    public void TakeCoin(Coin coin)
    {
        Destroy(coin.gameObject);
        CoinPickUp.Invoke();
    }
}