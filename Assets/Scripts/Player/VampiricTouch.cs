using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VampiricTouch : Skill
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _searchingCircle;
    [SerializeField] private int _healthPointsPerSecond = 5;
    [SerializeField] private float _respounceFrequency = 0.5f;
    [SerializeField] private int _duration = 6;

    private Enemy _enemy;
    private Coroutine _stealingHealth;
    private bool _isUsing = false;

    public KeyCode AssignedKey {  get; }

    private void OnDisable()
    {
        if (_stealingHealth != null)
            StopCoroutine(_stealingHealth);
    }

    public void Use()
    {
        if (_isUsing)
            return;

        if (TryFindEnemy())
        {
            _isUsing = true;
            _stealingHealth = StartCoroutine(StealHealth());
        }
    }

    private bool TryFindEnemy()
    {
        int diameterDivider = 2;
        float serchingRadius = _searchingCircle.transform.localScale.x / diameterDivider;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, serchingRadius);

        foreach (Collider2D collider in colliders)
            if (collider.TryGetComponent(out Enemy enemy))
            {
                _enemy = enemy;
                return true;
            }

        return false;
    }

    private IEnumerator StealHealth()
    {
        int passedTime = 0;
        WaitForSeconds timer = new WaitForSeconds(_respounceFrequency);

        while (passedTime < _duration && _enemy.isActiveAndEnabled && _player.isActiveAndEnabled)
        {
            _enemy.TakeDamage(_healthPointsPerSecond);
            _player.TakeHeal(_healthPointsPerSecond);

            passedTime++;

            yield return timer;
        }

        _isUsing = false;
        yield break;
    }
}
