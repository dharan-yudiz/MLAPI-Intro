using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public static Action OnCoinCollected;

    public static void CoinCollected() {
        OnCoinCollected?.Invoke();
    }
}
