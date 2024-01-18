using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Player))]
public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected KeyCode _activationKey;
    [SerializeField] protected float Damage;

    protected Player SkillUser;
    protected Enemy Target;

    protected bool IsActive = false;

    public void Start()
    {
        SkillUser = GetComponent<Player>();
    }

    public abstract void Activate();

    public virtual void Deactivate() => IsActive = false;

    public KeyCode GetActivationKeyCode() => _activationKey;
}
