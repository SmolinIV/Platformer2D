using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    private readonly string _targetTag = "Player";

    [SerializeField ]private RayStartPoint _rayStartPoint;
    private RaycastHit2D _detectedObject;
    private float _rayLength;

    void Start()
    {
        int rayIncreasingCoefficient = 15;
        _rayLength = transform.localScale.x * rayIncreasingCoefficient;
    }

    public bool TryFindPlayer()
    {
        _detectedObject = Physics2D.Raycast(_rayStartPoint.transform.position, _rayStartPoint.transform.right, _rayLength);

        if (_detectedObject.transform != null)
        {
            if (_detectedObject.transform.tag == _targetTag)
                return true;
        }

        return false;
    }
}