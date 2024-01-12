using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSliderSmooth : HealthBarSlider
{
    [SerializeField] protected int _smoothnessCoefficient = 1;

    protected override void SetSliderPosition()
    {
         _slider.value = Mathf.MoveTowards(_lastShowingHealth, _currentHealth, _smoothnessCoefficient * Time.deltaTime);
        _lastShowingHealth = _slider.value;
    }
}
