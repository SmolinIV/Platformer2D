using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSliderSmooth : HealthBarSlider
{
    [SerializeField] protected int SmoothnessCoefficient = 1;

    private Coroutine _calculatingSliderPosition;

    private float LastShowingHealth;

    private void OnDisable()
    {
        base.OnDisable();

        if (_calculatingSliderPosition != null)
            StopCoroutine(_calculatingSliderPosition);
    }

    private void Start()
    {
        base.Start();
        LastShowingHealth = TargetHealth.Current/TargetHealth.Max;
    }

    protected override void UpdateCondition()
    {
        _calculatingSliderPosition = StartCoroutine(CalculateSliderPosition());

    }

    private IEnumerator CalculateSliderPosition()
    {
        while (LastShowingHealth != TargetHealth.Current / TargetHealth.Max)
        {
            Slider.value = Mathf.MoveTowards(LastShowingHealth, TargetHealth.Current/TargetHealth.Max, SmoothnessCoefficient * Time.deltaTime);
            LastShowingHealth = Slider.value;

            yield return null;
        }
    }
}
