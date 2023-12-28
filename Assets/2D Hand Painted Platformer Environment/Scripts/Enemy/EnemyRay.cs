using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRay : MonoBehaviour
{
    private RayStartPoint _rayStartPoint;
    private RaycastHit2D _target;
    private float _rayLength;

    void Start()
    {
        _rayStartPoint = GetComponentInChildren<RayStartPoint>();
        int rayIncreasingCoefficient = 10;
        _rayLength = transform.localScale.x * rayIncreasingCoefficient;
    }

    public bool TryFindPlayer()
    {
        _target = Physics2D.Raycast(_rayStartPoint.transform.position, _rayStartPoint.transform.right, _rayLength);

        if (_target.collider == null)
            return false;

        if (_target.collider.gameObject.TryGetComponent(out Player player))
            return true;

        return false;

    }
}
