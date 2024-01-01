using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _coinCount;

    public void PutCoin() => _coinCount++;
}
