using System;
using System.Collections.Generic;
using UnityEngine;

public class ScaleModel : SceneObjectParameterModel {

    private Vector3 Scale;

    public override event EventHandler OnUpdateSceneObjectView;

    public ScaleModel() : base(Constants.SceneObjectParameter.SCALE)
    {
        Scale = new Vector3(1, 1, 1);
        RandomizationTypes.Add(null);
        RandomizationTypes.Add(null);
        RandomizationTypes.Add(null);
    }


    public override void InitFromGameObject(GameObject sceneObject)
    {
        Scale = sceneObject.transform.localScale;
    }
    public override GameObject GetGameObjectView()
    {
        var factory = new ParameterViewFactory();
        return factory.CreateScaleView();
    }

    public override void StoreAndApplyValueOnSceneObject(StoreParameterEventArgs args)
    {
        StoreValue(args);
        OnUpdateSceneObjectView(this, EventArgs.Empty);
    }

    public override object GetValue()
    {
        return Scale;
    }

    public override void UpdateParameterOnSceneObject(GameObject sceneObject)
    {
        sceneObject.transform.localScale = Scale;
    }

    public override void StoreValue(StoreParameterEventArgs args)
    {
        Scale = (Vector3)args.Value;
    }
    public override void RandomizeObject()
    {
        if (RandomizationTypes[0] != null && RandomizationTypes[0].IsRandom)
        {
            Scale.x = (float)RandomizationTypes[0].GetRandomValue();
        }

        if (RandomizationTypes[1] != null && RandomizationTypes[1].IsRandom)
        {
            Scale.y = (float)RandomizationTypes[1].GetRandomValue();
        }

        if (RandomizationTypes[2] != null && RandomizationTypes[2].IsRandom)
        {
            Scale.z = (float)RandomizationTypes[2].GetRandomValue();
        }
        OnUpdateSceneObjectView(this, EventArgs.Empty);
    }
}
