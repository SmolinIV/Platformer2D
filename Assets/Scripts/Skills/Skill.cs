using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected float Damage;

    protected Player SkillUser;
    protected Enemy Target;

    protected bool IsActive = false;

    public KeyCode ActivationKey { get; protected set; }

    public void Start()
    {
        SkillUser = GetComponent<Player>();
    }


    public abstract void Activate();

    public void Deactivate() => IsActive = false;
}
