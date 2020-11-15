using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Scripting.Pipeline;

public class SceneObjectView : MonoBehaviour, ISceneObjectView
{
    public event EventHandler<StoreParameterEventArgs> OnParameterValueChanged = (sender ,e) => {};
    public event EventHandler<StoreParameterEventArgs> OnSceneObjectValueChanged = (sender, e) => { };
    public event Action OnDeleteSceneObject;
    public event EventHandler OnDestroyController = (sender, e) => { };
    public event Action TestRandomize;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void DestoyController()
    {
        OnDestroyController(this, EventArgs.Empty);
    }

    public bool IsEquals(GameObject a)
    {
        return a == null ? false : a == gameObject;
    }

    public void ParameterValueChanged(object sender, StoreParameterEventArgs e)
    {
        OnParameterValueChanged(this, e);
    }

    public void SceneObjectValueChanged(string parameterType, object value)
    {
        OnSceneObjectValueChanged(this, new StoreParameterEventArgs(parameterType, value));
    }

    public void TestRandomizeObject()
    {
        TestRandomize();
    }

    public void DeleteSceneObject()
    {
        OnDeleteSceneObject();
    }
}
