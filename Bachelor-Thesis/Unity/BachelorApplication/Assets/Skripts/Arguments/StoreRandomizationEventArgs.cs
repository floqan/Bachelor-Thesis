using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRandomizationEventArgs : EventArgs
{
    public RandomizationType Type { get; set; }
    public int ListIndex { get; set; }
    public StoreRandomizationEventArgs(RandomizationType type, int index)
    {
        Type = type;
        ListIndex = index;
    }
}
