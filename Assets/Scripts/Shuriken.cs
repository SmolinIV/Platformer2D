using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]

public class Shuriken : MonoBehaviour
{
    [SerializeField] private GameObject _thrower;
    [SerializeField] private int _rotationSpeed;
    [SerializeField] private int _damage = 10;

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private Collider2D[] _throwerColliders2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _throwerColliders2D = _thrower.GetComponents<Collider2D>();
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
        foreach (Collider2D collider in _throwerColliders2D)
            Physics2D.IgnoreCollision(collider, _collider2D, true);

        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime); 
    }

    public void StartFlying(Vector2 direction) => _rigidbody2D.AddForce(direction);
}