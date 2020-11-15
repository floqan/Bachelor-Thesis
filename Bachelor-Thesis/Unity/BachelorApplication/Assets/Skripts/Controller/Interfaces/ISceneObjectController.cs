using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneObjectController 
{
    event EventHandler<SceneObjectSelectedEventArgs> OnSendParameter;
    event EventHandler<DeleteSceneObjectEventArgs> OnDeleteSceneObject;
    event EventHandler OnDeleteReferences;
    event EventHandler OnUpdateArrow;
    void RequestParameter(object sender, SceneObjectSelectedEventArgs e);
    void DestoryController(object sender, EventArgs e);
    void ParameterValueChanged(object sender, StoreParameterEventArgs e);
    void CreateImage();
    void SetMainCamera();
    void SetDefaultCamera();
    ISceneObjectView getView();
}
