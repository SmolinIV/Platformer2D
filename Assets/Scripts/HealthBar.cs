using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthBar : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] protected GameObject _target;

    protected Health _targetHealth;

    protected int _maxHealth = 0;
    protected int _currentHealth = 0;
    protected float _lastShowingHealth = 0;

    protected void Start()
    {
        if (_target.TryGetComponent(out Health health))
        {
            _targetHealth = health;
            _maxHealth = health.MaxHealth;
            _currentHealth = health.CurrentHealth;
            _lastShowingHealth = _currentHealth;
        }
    }

    protected void Update()
    {
        _maxHealth = _targetHealth.MaxHealth;
        _currentHealth = _targetHealth.CurrentHealth;

        UpdateHealthBar();
    }

    public void ChangeCurrentHealth(int currentHealth)
    {
        _currentHealth = currentHealth;
    }

    protected abstract void UpdateHealthBar();
=======
    [SerializeField] protected Health TargetHealth;

    public void OnDisable()
    {
        TargetHealth.HealthChaged -= UpdateCondition;
    }

    protected void Start()
    {
        TargetHealth.HealthChaged += UpdateCondition;
    }

    protected abstract void UpdateCondition();
>>>>>>> Stashed changes
}
