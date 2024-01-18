using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class VampiricTouch : Skill
{
    [SerializeField] private float _scopeRadius = 3f;
    [SerializeField] private int duration;
    [SerializeField] private LifeBall _lifeBall;

    private Coroutine _takingLifePoints;

    public void Start()
    {
        base.Start();
    }

    private void OnDisable()
    {
        if (_takingLifePoints != null)
            StopCoroutine( _takingLifePoints );
    }

    private void Update()
    {
        if (Target == null)
            return;

        if (Vector2.Distance(transform.position, Target.transform.position) > _scopeRadius)
            Deactivate();
    }

    public override void Activate()
    {
        if (IsActive)
            return;

        if (TryFindClosestTarget())
        {
            IsActive = true;
            _takingLifePoints = StartCoroutine(TakeLifePoints());
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();

        if (_takingLifePoints != null)
            StopCoroutine(_takingLifePoints);

        Target = null;
    }

    private bool TryFindClosestTarget()
    {
        float minDistance = float.MaxValue;
        int minDistanceEnemyIndex = 0;

        Collider2D[] detectingResult = Physics2D.OverlapCircleAll(transform.position, _scopeRadius);
        List<Collider2D> enemies = detectingResult.Where(target => target.TryGetComponent(out Enemy enemy)).ToList();

        if (enemies.Count() == 0)
            return false;

        for (int i = 0; i < enemies.Count(); i++)
        {
            float estimatedDistance = Vector2.Distance(transform.position, enemies[i].transform.position);

            if (estimatedDistance < minDistance)
            {
                minDistance = estimatedDistance;
                minDistanceEnemyIndex = i;
            }
        }

        Target = enemies[minDistanceEnemyIndex].GetComponent<Enemy>();

        return true;
    }

    private IEnumerator TakeLifePoints()
    {
        int passedTime = 0;
        int frequency = 1;
        WaitForSeconds delay = new WaitForSeconds(frequency);

        while (passedTime < duration && Target != null && IsActive)
        {
            Target.TakeDamage(Damage);

           LifeBall newLifeBall = Instantiate(_lifeBall, Target.transform.position, Quaternion.identity);

            newLifeBall.Appear(SkillUser, Damage);

            passedTime++;

            yield return delay;
        }

        IsActive = false;
        Target = null;

        yield break;
    }
}
