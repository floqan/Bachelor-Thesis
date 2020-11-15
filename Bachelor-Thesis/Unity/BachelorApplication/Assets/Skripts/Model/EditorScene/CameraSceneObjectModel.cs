using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSceneObjectModel : AbstractSceneObjectModel
{

    public event Action OnCreateImage;
    public event Action OnSetMainCamera;
    public event Action OnSetDefaultCamera;

    public CameraSceneObjectModel(CameraItemModel itemModel): base(itemModel)
    {
        Parameter.Add(new PositionModel());
        Parameter.Add(new RotationModel());
    }

    public void CreateImage()
    {
        OnCreateImage?.Invoke();
    }

    public void SetMainCamera()
    {
        OnSetMainCamera();
    }

    public void SetDefaultCamera()
    {
        OnSetDefaultCamera();
    }
}
