using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBarText : HealthBar
{
    [SerializeField] protected TMP_Text _numericalIndicator;

    protected override void UpdateHealthBar()
    {
        if (_lastShowingHealth != _currentHealth)
            UpdateNumericalIndicator();
    }

    private void UpdateNumericalIndicator()
    {
        _numericalIndicator.text = _currentHealth.ToString() + "/" + _maxHealth.ToString();
        _lastShowingHealth = _currentHealth;
    }
}
