using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _maxHealth;

    public int CurrentHealth { get; private set; }

    private void Start()
    {
        _maxHealth = CurrentHealth = 100;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }

    public void Recover() => CurrentHealth = _maxHealth;
}