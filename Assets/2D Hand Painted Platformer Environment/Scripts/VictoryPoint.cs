using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPoint : MonoBehaviour
{
    [SerializeField] private Fan _fan;
    [SerializeField] private VictoryBunner _bunner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCondition player))
        {
            _fan.TurnOn();
            _bunner.Show();
        }
    }
}
