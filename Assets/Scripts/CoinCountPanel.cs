using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountPanel : MonoBehaviour
{
    private TextMeshPro _coinsShowingNumber;

    private int _coinsNumber;

    private void OnEnable() => CoinTaker.CoinPickUp += UpdateCoinsNumber;

    private void OnDisable() => CoinTaker.CoinPickUp -= UpdateCoinsNumber;

    private void Start()
    {
        _coinsShowingNumber = GetComponent<TextMeshPro>();
        _coinsShowingNumber.text = "0";
        _coinsNumber = 0;
    }

    public void UpdateCoinsNumber()
    {
        _coinsShowingNumber.text = (++_coinsNumber).ToString();
    }
}
