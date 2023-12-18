using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    private TextMeshPro _coinsNumber;

    private void Start()
    {
        _coinsNumber = GetComponent<TextMeshPro>();
        _coinsNumber.text = "0";
    }

    void Update()
    {
        _coinsNumber.text = _player.CoinsNumber.ToString();
    }
}
