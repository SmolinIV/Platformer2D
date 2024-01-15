using System;
using UnityEngine;

public class Health : MonoBehaviour
{
<<<<<<< Updated upstream
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        MaxHealth = CurrentHealth = 100;
=======
    public Action HealthChaged;

    public float Max { get; private set; }
    public float Current { get; private set; }

    private void Awake()
    {
        Max = 100;
        Current = Max;
>>>>>>> Stashed changes
    }

    public void TakeDamage(float damage)
    {
<<<<<<< Updated upstream
        CurrentHealth -= damage;

        if (CurrentHealth < 0)
         CurrentHealth = 0; 
    }

    public void TakeHeal(int healingPoints)
    {
        CurrentHealth += healingPoints;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
    }

    public void Recover() => TakeHeal(MaxHealth);
=======
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
>>>>>>> Stashed changes
}