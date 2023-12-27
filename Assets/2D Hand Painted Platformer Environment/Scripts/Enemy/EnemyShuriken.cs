using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShuriken : Shuriken
{
    private void Start()
    {
        Initialize(typeof(Player));
    }
}
