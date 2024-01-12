using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlider : HealthBar
{
    protected Slider _slider;

    protected void Start()
    {
        base.Start();
        _slider = GetComponent<Slider>();

        _slider.maxValue = _maxHealth;
        _slider.value = _lastShowingHealth = _currentHealth;
    }

    protected override void UpdateHealthBar()
    {
        if (_lastShowingHealth != _currentHealth)
            SetSliderPosition();
    }

    protected virtual void SetSliderPosition()
    {
        _slider.value = _currentHealth;
        _lastShowingHealth = _slider.value;
    }

}
