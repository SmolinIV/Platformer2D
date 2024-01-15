using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action HealthChaged;

    public float Max { get; private set; }
    public float Current { get; private set; }

    private void Awake()
    {
        Max = 100;
        Current = Max;
    }

    public void TakeDamage(float damage)
    {
        Current -= damage;

        if (Current < 0)
         Current = 0;

        HealthChaged?.Invoke();
    }

    public void TakeHeal(float healingPoints)
    {
        Current += healingPoints;

        if (Current > Max)
            Current = Max;

        HealthChaged?.Invoke();
    }

    public void Recover() => TakeHeal(Max);
}