using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlider : HealthBar
{
    protected Slider Slider;

    protected void Start()
    {
        base.Start();
        Slider = GetComponent<Slider>();

        Slider.minValue = 0;
        Slider.maxValue = 1;

        Slider.value = TargetHealth.Current/TargetHealth.Max;
    }

    protected override void UpdateCondition()
    {
        Slider.value = TargetHealth.Current/TargetHealth.Max;
    }
}
