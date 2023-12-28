using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShuriken : Shuriken
{
    private void Start()
    {
        InitializeTarget(typeof(Enemy));
    }
}