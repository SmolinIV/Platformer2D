using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTaker : MonoBehaviour
{
    [SerializeField] private CoinCountPanel _coinCountPanel;
    private int _coinsNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            TakeCoin(coin);
    }

    public void TakeCoin(Coin coin)
    {
        Destroy(coin.gameObject);
        ++_coinsNumber;
        _coinCountPanel.UpdateCoinsNumber(_coinsNumber);
    }
}