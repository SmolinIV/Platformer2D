using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Animator))]

public class VictoryBunner : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger("isShowing");
    }
}
