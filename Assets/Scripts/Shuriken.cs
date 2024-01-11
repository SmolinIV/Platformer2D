using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Shuriken : MonoBehaviour
{
    [SerializeField] private int _rotationSpeed;
    [SerializeField] private int _damage = 10;

    private Rigidbody2D _rigidbody2D;

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable character))
        {
            character.TakeDamage(_damage);
            gameObject.SetActive(false);
        }
        else if (collision.TryGetComponent(out PhisicalPlatform platform))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible() => gameObject.SetActive(false);

    private void Update()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime); 
    }

    public void StartFlying(Vector2 direction) => _rigidbody2D.AddForce(direction);

    public void InitializeThrower(GameObject thrower)
    {
        Collider2D[] throwerColloders = thrower.GetComponents<Collider2D>();

        foreach(Collider2D collider in throwerColloders)
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collider, true);
    }
}