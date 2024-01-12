using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        MaxHealth = CurrentHealth = 100;
    }

    public void TakeDamage(int damage)
    {
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
}