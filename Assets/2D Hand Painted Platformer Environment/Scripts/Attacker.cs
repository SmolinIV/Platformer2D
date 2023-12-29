using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ShurikenPool))]

public class Attacker : MonoBehaviour
{
    [SerializeField] private Shuriken _shurikenPrefab;
    [SerializeField] private Vector2 _throwingForce;
    private ShurikenPool _shurikenPool;

    private void Awake()
    {
        _shurikenPool = GetComponent<ShurikenPool>();
    }

    public void ThrowShuriken()
    {
        Vector2 direction = transform.right * _throwingForce;
        direction.y += _throwingForce.y;

        if(_shurikenPool.TryGetShuriken(out Shuriken spawnedShuriken))
            spawnedShuriken.StartFlying(direction);
    }

    private Shuriken CreateShuriken() => Instantiate(_shurikenPrefab);
}
