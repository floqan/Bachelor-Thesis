using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationModel : SceneObjectParameterModel
{
    private Vector3 Rotation;
    public override event EventHandler OnUpdateSceneObjectView;

    public RotationModel() : base(Constants.SceneObjectParameter.ROTATION)
    {
        Rotation = Vector3.zero;
        RandomizationTypes.Add(null);
        RandomizationTypes.Add(null);
        RandomizationTypes.Add(null);
    }

    public override void InitFromGameObject(GameObject sceneObject)
    {
        Rotation = sceneObject.transform.rotation.eulerAngles;
    }
    public override GameObject GetGameObjectView()
    {
        ParameterViewFactory factory = new ParameterViewFactory();
        return factory.CreateRotationView();
    }

    public override object GetValue()
    {
        return Rotation;
    }

    public override void StoreAndApplyValueOnSceneObject(StoreParameterEventArgs args)
    {
        StoreValue(args);
        OnUpdateSceneObjectView(this, EventArgs.Empty);
    }

    public override void StoreValue(StoreParameterEventArgs args)
    {
        Vector3 rotation = (Vector3)args.Value;
        Rotation = rotation;
    }

    public override void UpdateParameterOnSceneObject(GameObject sceneObject)
    {
        sceneObject.transform.rotation = Quaternion.Euler(Rotation);
    }
    public override void RandomizeObject()
    {
        if (RandomizationTypes[0] != null && RandomizationTypes[0].IsRandom)
        {
            Rotation.x = (float)RandomizationTypes[0].GetRandomValue();
        }

        if (RandomizationTypes[1] != null && RandomizationTypes[1].IsRandom)
        {
            Rotation.y = (float)RandomizationTypes[1].GetRandomValue();
        }

        if (RandomizationTypes[2] != null && RandomizationTypes[2].IsRandom)
        {
            Rotation.z = (float)RandomizationTypes[2].GetRandomValue();
        }
        OnUpdateSceneObjectView(this, EventArgs.Empty);
    }
}
