using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneObjectView
{
    //reagiert auf Änderungen des SceneObject, wie Positionsänderung oder Rotation

    event EventHandler<StoreParameterEventArgs> OnParameterValueChanged;
    event EventHandler<StoreParameterEventArgs> OnSceneObjectValueChanged;
    event Action OnDeleteSceneObject;
    event EventHandler OnDestroyController;
    event Action TestRandomize;
    bool IsEquals(GameObject a);
    void ParameterValueChanged(object sender, StoreParameterEventArgs e);
    GameObject GetGameObject();
    void SceneObjectValueChanged(string parameterType, object value);
    void TestRandomizeObject();
    void DestoyController();
    void DeleteSceneObject();
}
