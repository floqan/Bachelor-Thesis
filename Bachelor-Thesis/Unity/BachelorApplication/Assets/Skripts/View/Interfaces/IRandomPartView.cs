using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRandomPartView 
{
    List<RandomizationType> Types { get; set; }

    void InitTypes();
    RandomizationType GetSelectedType();
    void SelectType(RandomizationType type);
    bool IsChecked();
}
