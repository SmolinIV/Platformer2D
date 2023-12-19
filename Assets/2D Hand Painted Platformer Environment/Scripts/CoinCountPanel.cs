using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountPanel : MonoBehaviour
{
    private TextMeshPro _coinsNumber;

    private void Start()
    {
        _coinsNumber = GetComponent<TextMeshPro>();
        _coinsNumber.text = "0";
    }

    public void UpdateCoinsNumber(int coinsNumber)
    {
        _coinsNumber.text = coinsNumber.ToString();
    }
}
