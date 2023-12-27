using UnityEngine;


public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    private SpawnPoint[] _spawnPoints;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        CreateCoins();
    }

    private void CreateCoins()
    {
        foreach (SpawnPoint spawnPoint in _spawnPoints)
            Instantiate(_coinPrefab, spawnPoint.transform);
    }
}
