using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LifeBall : MonoBehaviour
{
    [SerializeField] private int _speed = 4;
    private Player _target;
    
    public float HealthPoints { get; private set; }

    private void Update()
    {
        if (_target == null)
            return;

        if (transform.position == _target.transform.position || _target.IsDied)
        {
            _target.TakeHeal(HealthPoints);
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.localPosition, _target.transform.position, _speed * Time.deltaTime);
        }  
    }

    public void Appear(Player player, float healthPoints)
    {
        HealthPoints = healthPoints;
        _target = player;
    }
}
