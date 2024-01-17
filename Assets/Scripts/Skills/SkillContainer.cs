using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContainer : MonoBehaviour
{
    [SerializeField] private Skill[] _skills;

    public IReadOnlyCollection<Skill> GetAllSkills() => _skills;

    public void DeactiveAllSkills()
    {
        foreach (Skill skill in _skills)
           skill.Deactivate();
    }
}
