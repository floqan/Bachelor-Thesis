using System;
using System.Collections.Generic;
using UnityEngine;

public class LightningRangeModel : SceneObjectParameterModel
{

    private float Range;

    public override event EventHandler OnUpdateSceneObjectView = (sender, e) => { };

    public LightningRangeModel() : base(Constants.SceneObjectParameter.LIGHTNING_RANGE)
    {
        Range = 50;
        RandomizationTypes.Add(null);
    }

    public override GameObject GetGameObjectView()
    {
        ParameterViewFactory factory = new ParameterViewFactory();
        return factory.CreateLightningRangeView();
    }

    public override void StoreAndApplyValueOnSceneObject(StoreParameterEventArgs args)
    {
        StoreValue(args);
        OnUpdateSceneObjectView(this, args);
    }

    public override void StoreValue(StoreParameterEventArgs args)
    {
        Range = (float)args.Value;
    }

    public override object GetValue()
    {
        return Range;
    }

    public override void UpdateParameterOnSceneObject(GameObject sceneObject)
    {
        Light light = sceneObject.GetComponent<Light>();
        if(light != null)
        {
            light.range = Range;
        }
    }

    public override void InitFromGameObject(GameObject sceneObject)
    {
        Light light = sceneObject.GetComponent<Light>();
        if (light != null)
        {
            light.range = Range;
        }
    }
    public override void RandomizeObject()
    {
        if (RandomizationTypes[0] != null && RandomizationTypes[0].IsRandom)
        {
            Range = (float)RandomizationTypes[0].GetRandomValue();
        }
        OnUpdateSceneObjectView(this, EventArgs.Empty);
    }
}
