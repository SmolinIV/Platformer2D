using System.Collections;
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
        ActivationKey = KeyCode.LeftControl;
    }

    private void OnDisable()
    {
        if (_takingLifePoints != null)
            StopCoroutine( _takingLifePoints );
    }

    public override void Activate()
    {
        if (IsActive)
            return;

        Collider2D[] detectingResult = Physics2D.OverlapCircleAll(transform.position, _scopeRadius);

        foreach (Collider2D target in detectingResult)
        {
            if (target.TryGetComponent(out Enemy enemy))
            {
                Target = enemy;
                IsActive = true;

                _takingLifePoints = StartCoroutine(TakeLifePoints());

                break;
            }
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();

        if (_takingLifePoints != null)
            StopCoroutine(_takingLifePoints);

        Target = null;
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
