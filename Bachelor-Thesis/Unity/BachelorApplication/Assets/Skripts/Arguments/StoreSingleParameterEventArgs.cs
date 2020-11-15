using System;
using System.Collections.Generic;
using UnityEngine;
public class StoreParameterEventArgs : EventArgs
{
    public object Value { get; set; }
    public string ParameterType { get; }

    public StoreParameterEventArgs(string parameterType)
    {
        ParameterType = parameterType;
    }
    public StoreParameterEventArgs(string parameterType, object value)
    {
        ParameterType = parameterType;
        Value = value;
    }
}