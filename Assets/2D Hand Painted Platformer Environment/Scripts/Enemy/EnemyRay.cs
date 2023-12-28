using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRay : MonoBehaviour
{
    private Collider2D _collider2D;
    private RaycastHit2D _target;
    private float _rayLength;

    void Start()
    {
        int rayIncreasingCoefficient = 10;
        _rayLength = transform.localScale.x * rayIncreasingCoefficient;
        _collider2D = GetComponent<Collider2D>();
    }

    public bool TryFindPlayer()
    {
        _target = Physics2D.Raycast(transform.position, transform.forward, _rayLength);
        
        if (_target.collider.gameObject.GetType() == typeof(Player))
            return true;

        return false;
    }
}
