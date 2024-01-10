using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]

public class Shuriken : MonoBehaviour
{
    [SerializeField] private GameObject _throwerPrefab;
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
            if (Equals(PrefabUtility.GetPrefabObject(_throwerPrefab), (object)character))
                return;

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
}