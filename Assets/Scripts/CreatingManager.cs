using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingManager : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private HealthBarText _healthBarText;
    [SerializeField] private HealthBarSlider _healthBarSlider;
    [SerializeField] private HealthBarSliderSmooth _healthBarSliderSmooth;

    private void Start()
    {
        _health.enabled = true;
        _healthBarSlider.enabled = true;
        _healthBarText.enabled = true;
        _healthBarSliderSmooth.enabled = true;
    }
}
