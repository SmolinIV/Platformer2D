using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class Shuriken : MonoBehaviour
{
    [SerializeField] private int _rotationSpeed;
    private Type _targetType;

    private Rigidbody2D _rigidbody2D;

    private int _damage;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _damage = 50;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable character))
        {
            if (character.GetType() == _targetType)
            {
                character.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
        }
        else if (collision.TryGetComponent(out PhisicalPlatform platform))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible() => gameObject.SetActive(false);

    private void Update() => transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);

    public void StartFlying(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction);
    }

    protected void Initialize(Type targetType)
    {
        _targetType = targetType;
    }
}