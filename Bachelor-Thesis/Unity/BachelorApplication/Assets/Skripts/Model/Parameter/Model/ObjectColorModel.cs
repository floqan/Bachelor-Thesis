using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColorModel : SceneObjectParameterModel
{
    public override event EventHandler OnUpdateSceneObjectView = (sender, e) => { };

    public Color Color;


    public ObjectColorModel() : base(Constants.SceneObjectParameter.OBJECT_COLOR)
    {
        Color = Color.white;
        RandomizationTypes.Add(null);
        RandomizationTypes.Add(null);
        RandomizationTypes.Add(null);
    }

    public override GameObject GetGameObjectView()
    {
        ParameterViewFactory factory = new ParameterViewFactory();
        return factory.CreateObjectColorView();
    }

    public override object GetValue()
    {
        return Color;
    }

    public override void InitFromGameObject(GameObject sceneObject)
    {
        Color = sceneObject.GetComponent<Renderer>().material.color;
    }

    public override void StoreAndApplyValueOnSceneObject(StoreParameterEventArgs args)
    {
        StoreValue(args);
        OnUpdateSceneObjectView(this, args);
    }

    public override void StoreValue(StoreParameterEventArgs args)
    {
        Color = (Color)args.Value;
    }

    public override void UpdateParameterOnSceneObject(GameObject sceneObject)
    {
        sceneObject.GetComponent<Renderer>().material.color = Color;
    }
    public override void RandomizeObject()
    {
        if (RandomizationTypes[0] != null && RandomizationTypes[0].IsRandom)
        {
            Color.r = (float)RandomizationTypes[0].GetRandomValue() / 256;
        }

        if (RandomizationTypes[1] != null && RandomizationTypes[1].IsRandom)
        {
            Color.g = (float)RandomizationTypes[1].GetRandomValue() / 256;
        }

        if (RandomizationTypes[2] != null && RandomizationTypes[2].IsRandom)
        {
            Color.b = (float)RandomizationTypes[2].GetRandomValue() / 256;
        }
        OnUpdateSceneObjectView(this, EventArgs.Empty);
    }
}
