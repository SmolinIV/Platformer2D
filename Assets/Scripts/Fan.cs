using System.Collections;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField, Min(1)] private int _rotationSpeed;

    private Coroutine _rotating;

    bool _isWorking = false;

    private void OnDestroy()
    {
        if (_rotating != null)
            StopCoroutine(_rotating);
    }

    public void TurnOn()
    {
        _isWorking = true;
        _rotating = StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (_isWorking)
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
