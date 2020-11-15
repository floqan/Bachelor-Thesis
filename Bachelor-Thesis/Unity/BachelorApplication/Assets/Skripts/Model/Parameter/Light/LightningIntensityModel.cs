using System;
using System.Collections.Generic;
using UnityEngine;

public class LightningIntensityModel : SceneObjectParameterModel
{
    private float Intensity;
    public override event EventHandler OnUpdateSceneObjectView = (sender, e) => { };

    public LightningIntensityModel() : base(Constants.SceneObjectParameter.LIGHTNING_INTENSITY)
    {
        Intensity = 10;
        RandomizationTypes.Add(null);
    }


    public override GameObject GetGameObjectView()
    {
        ParameterViewFactory factory = new ParameterViewFactory();
        return factory.CreateLightningIntensityView();
    }

    public override object GetValue()
    {
        return Intensity;
    }

    public override void InitFromGameObject(GameObject sceneObject)
    {
        Light light = sceneObject.GetComponent<Light>();
        if(light != null)
        {
            Intensity = light.intensity;
        }
    }

    public override void StoreAndApplyValueOnSceneObject(StoreParameterEventArgs args)
    {
        StoreValue(args);
        OnUpdateSceneObjectView(this, args);
    }

    public override void StoreValue(StoreParameterEventArgs args)
    {
        Intensity = (float) args.Value;
    }

    public override void UpdateParameterOnSceneObject(GameObject sceneObject)
    {
        Light light = sceneObject.GetComponent<Light>();
        if(light != null)
        {
            light.intensity = Intensity;
        }
    }
    public override void RandomizeObject()
    {
        if (RandomizationTypes[0] != null && RandomizationTypes[0].IsRandom)
        {
            Intensity = (float)RandomizationTypes[0].GetRandomValue();
        }
        OnUpdateSceneObjectView(this, EventArgs.Empty);
    }
}
