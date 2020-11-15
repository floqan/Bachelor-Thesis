using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneObjectParameterController 
{
    void UpdateParameterView(object sender, EventArgs e);
    void RandomizationChanged(object sender, StoreRandomizationEventArgs e);
}
