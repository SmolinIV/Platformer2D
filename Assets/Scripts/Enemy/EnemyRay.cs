using UnityEngine;

public class EnemyRay : MonoBehaviour
{
    private readonly string _targetTag = "Player";

    private RayStartPoint _rayStartPoint;
    private RaycastHit2D _findObject;
    private float _rayLength;

    void Start()
    {
        _rayStartPoint = GetComponentInChildren<RayStartPoint>();
        int rayIncreasingCoefficient = 15;
        _rayLength = transform.localScale.x * rayIncreasingCoefficient;
    }

    public bool TryFindPlayer()
    {
        _findObject = Physics2D.Raycast(_rayStartPoint.transform.position, _rayStartPoint.transform.right, _rayLength);

        if (_findObject.transform != null)
        {
            if (_findObject.transform.tag == _targetTag)
                return true;
        }

        return false;
    }
}