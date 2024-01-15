using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShurikenPool : MonoBehaviour
{
    [SerializeField] private Shuriken _shurikenPrefab;
    [SerializeField] private int _shurikenCount = 10;

    private List<Shuriken> _shurikens;

    private void Awake()
    {
        _shurikens = new List<Shuriken>();

        for (int i = 0; i < _shurikenCount; i++)
        {
            _shurikens.Add(Instantiate(_shurikenPrefab));
            _shurikens[i].gameObject.SetActive(false);
        }
    }

    public bool TryGetShuriken(out Shuriken spawnedShuriken)
    {
        spawnedShuriken = null;

        foreach (Shuriken shuriken in _shurikens)
        {
            if (shuriken.gameObject.activeSelf == false)
            {
                spawnedShuriken = shuriken;
                spawnedShuriken.transform.position = transform.position;
                spawnedShuriken.InitializeThrower(gameObject);
                spawnedShuriken.gameObject.SetActive(true);

                return true;
            }
        }

        return false;
    }
}
