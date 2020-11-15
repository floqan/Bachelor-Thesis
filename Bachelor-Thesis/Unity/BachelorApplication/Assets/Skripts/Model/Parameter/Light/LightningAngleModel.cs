using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAngleModel : SceneObjectParameterModel
{
    
    float Angle { get; set; }
    public override event EventHandler OnUpdateSceneObjectView = (sender, e) => { };

    public LightningAngleModel() : base(Constants.SceneObjectParameter.LIGHTNING_SPOT_ANGLE)
    {
        Angle = 60;
        RandomizationTypes.Add(null);
    }


    public override GameObject GetGameObjectView()
    {
        ParameterViewFactory factory = new ParameterViewFactory();
        return factory.CreateLightningAngleView();
    }

    public override object GetValue()
    {
        return Angle;
    }

    public override void InitFromGameObject(GameObject sceneObject)
    {
        Light light = sceneObject.GetComponent<Light>();
        if(light != null)
        {
            Angle = light.spotAngle;
        }
    }

    public override void StoreAndApplyValueOnSceneObject(StoreParameterEventArgs args)
    {
        StoreValue(args);
        OnUpdateSceneObjectView(this, args);
    }

    public override void StoreValue(StoreParameterEventArgs args)
    {
        Angle = (float) args.Value;
    }

    public override void UpdateParameterOnSceneObject(GameObject sceneObject)
    {
        Light light = sceneObject.GetComponent<Light>();
        if(light != null)
        {
            light.spotAngle = Angle;
        }
    }
    public override void RandomizeObject()
    {
        if (RandomizationTypes[0] != null && RandomizationTypes[0].IsRandom)
        {
            Angle = (float)RandomizationTypes[0].GetRandomValue();
        }
        OnUpdateSceneObjectView(this, EventArgs.Empty);
    }
}
