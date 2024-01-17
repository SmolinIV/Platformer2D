using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float damage);

    void TakeHeal(float healingPoint);
}