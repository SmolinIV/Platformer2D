using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBarText : HealthBar
{
    [SerializeField] private TMP_Text _numericalIndicator;

    protected override void UpdateCondition()
    {
        _numericalIndicator.text = ((int)TargetHealth.Current).ToString() + "/" + ((int)TargetHealth.Max).ToString();
    }
}
