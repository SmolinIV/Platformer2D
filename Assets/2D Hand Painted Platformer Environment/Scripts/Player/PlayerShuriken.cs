using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShuriken : Shuriken
{
    private void Start()
    {
        Initialize(typeof(Enemy));
    }
}