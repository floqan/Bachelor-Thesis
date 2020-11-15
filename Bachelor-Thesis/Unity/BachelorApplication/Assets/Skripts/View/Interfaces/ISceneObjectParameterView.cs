using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface ISceneObjectParameterView 
{
    string ParameterType { get; set; }

    event EventHandler<StoreParameterEventArgs> OnParameterValueChanged;
    event EventHandler<StoreRandomizationEventArgs> OnRandomizationChanged;

    //Initialize the view
    void Init(object value, List<RandomizationType> randomizationTypes);
    
    //Assign Method to On of the input fields and handle change of input
    void ParameterValueChanged();

    //Update the fields of the view with changed data
    void UpdateParameterView(object value);
}
