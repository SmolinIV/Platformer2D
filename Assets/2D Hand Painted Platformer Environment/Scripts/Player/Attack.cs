using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ShurikenPool))]

public class Attack : MonoBehaviour
{
    [SerializeField] private Shuriken _shurikenPrefab;

    private ShurikenPool _shurikenPool;

    private void Awake()
    {
        _shurikenPool = GetComponent<ShurikenPool>();
    }

    public void ThrowShuriken()
    {
        float forceByAxisX = 250;
        float forceByAxisY = 70;

        if (transform.rotation.y != 0)
            forceByAxisX *= -1;

        if(_shurikenPool.TryGetShuriken(out Shuriken spawnedShuriken))
            spawnedShuriken.StartFlying(new Vector2(forceByAxisX, forceByAxisY));
    }

    private Shuriken CreateShuriken() => Instantiate(_shurikenPrefab);

}
