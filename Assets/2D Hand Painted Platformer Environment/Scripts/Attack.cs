using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ShurikenPool))]

public class Attack : MonoBehaviour
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
        if (transform.rotation.y != 0)
            _throwingForce.x *= -1;

        if(_shurikenPool.TryGetShuriken(out Shuriken spawnedShuriken))
            spawnedShuriken.StartFlying(_throwingForce);
    }

    private Shuriken CreateShuriken() => Instantiate(_shurikenPrefab);
}
