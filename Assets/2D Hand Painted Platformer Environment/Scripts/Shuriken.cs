using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Shuriken : MonoBehaviour
{
    [SerializeField] private int _rotationSpeed;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!collision.TryGetComponent(out Player player))
    //        Destroy(gameObject);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.TryGetComponent(out Player player))
            Destroy(gameObject);
    }

    private void Update() => transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);

    public void StartFlying(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction);
    }
}