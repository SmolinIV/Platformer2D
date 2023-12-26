using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Shuriken _shuriken;

    public void ThrowShuriken()
    {
        float forceByAxisX = 400;
        float forceByAxisY = 100;

        if (transform.rotation.y != 0)
            forceByAxisX *= -1;

        Shuriken newShuriken = Instantiate(_shuriken, transform.position, Quaternion.identity);

        newShuriken.StartFlying(new Vector2(forceByAxisX, forceByAxisY));
    }
}
