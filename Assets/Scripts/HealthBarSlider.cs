using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlider : HealthBar
{
<<<<<<< Updated upstream
    protected Slider _slider;
=======
    protected Slider Slider;
>>>>>>> Stashed changes

    protected void Start()
    {
        base.Start();
<<<<<<< Updated upstream
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

=======
        Slider = GetComponent<Slider>();

        Slider.minValue = 0;
        Slider.maxValue = 1;

        Slider.value = TargetHealth.Current/TargetHealth.Max;
    }

    protected override void UpdateCondition()
    {
        Slider.value = TargetHealth.Current/TargetHealth.Max;
    }
>>>>>>> Stashed changes
}
