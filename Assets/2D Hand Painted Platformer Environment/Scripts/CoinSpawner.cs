using UnityEngine;


public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    private SpawnPoint[] _spawnPoints;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        CreateCoins();
    }

    private void CreateCoins()
    {
        foreach (SpawnPoint spawnPoint in _spawnPoints)
            Instantiate(_coin, spawnPoint.transform);
    }
}
