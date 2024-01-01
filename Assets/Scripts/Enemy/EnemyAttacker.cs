using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    private ShurikenThrower _shurikenThrower;

    private Coroutine _attacking;

    private void Start()
    {
        _shurikenThrower = GetComponent<ShurikenThrower>();
    }

    private void OnDisable()
    {
        if (_attacking != null)
            StopCoroutine(_attacking);
    }

    public void Attack() => _attacking = StartCoroutine(CyclicalAttack());

    public void StopAttack()
    {
        if (_attacking != null)
            StopCoroutine(_attacking);
    }

    private IEnumerator CyclicalAttack()
    {
        int attackDelay = 1;

        while (true)
        {
            _shurikenThrower.ThrowShuriken();
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
