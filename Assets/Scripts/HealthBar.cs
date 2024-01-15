using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthBar : MonoBehaviour
{
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
}
