using UnityEngine;
using UnityEngine.Pool;

public class Attack : MonoBehaviour
{
    [SerializeField] private Shuriken _shurikenPrefab;

    private ObjectPool<Shuriken> _pooledShuriken;

    public void Awake()
    {
        //var ShurikenPool = new ObjectPool<Shuriken>(CreateShuriken, )
    }

    public void ThrowShuriken()
    {
        float forceByAxisX = 250;
        float forceByAxisY = 70;

        if (transform.rotation.y != 0)
            forceByAxisX *= -1;

        Shuriken newShuriken = Instantiate(_shurikenPrefab, transform.position, Quaternion.identity);

        newShuriken.StartFlying(new Vector2(forceByAxisX, forceByAxisY));
    }

    private Shuriken CreateShuriken() => Instantiate(_shurikenPrefab, transform.position, Quaternion.identity);
    
    private void GetNewShuriken(Shuriken shuriken)
    {
        shuriken.transform.position = transform.position;
    }
}
