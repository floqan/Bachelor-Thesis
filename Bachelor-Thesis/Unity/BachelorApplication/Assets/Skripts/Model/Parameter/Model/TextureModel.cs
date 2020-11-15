using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureModel : SceneObjectParameterModel
{
    Material Material;
    public override event EventHandler OnUpdateSceneObjectView = (sender, e) => { };

    public TextureModel() : base(Constants.SceneObjectParameter.TEXTURE)
    {
        RandomizationTypes.Add(null);
    }

    public override void InitFromGameObject(GameObject sceneObject)
    {
        Material = sceneObject.GetComponent<Renderer>().material;
    }
    public override GameObject GetGameObjectView()
    {
        ParameterViewFactory factory = new ParameterViewFactory();
        return factory.CreateTextureView();
    }

    public override object GetValue()
    {
        return Material;
    }

    public override void StoreAndApplyValueOnSceneObject(StoreParameterEventArgs args)
    {
        StoreValue(args);
        OnUpdateSceneObjectView(this, EventArgs.Empty);
    }

    public override void StoreValue(StoreParameterEventArgs args)
    {
        Material = (Material)args.Value;
    }

    public override void UpdateParameterOnSceneObject(GameObject sceneObject)
    {
        Color color = sceneObject.GetComponent<Renderer>().material.color;
        sceneObject.GetComponent<Renderer>().material = Material;
        sceneObject.GetComponent<Renderer>().material.color = color;
    }
    public override void RandomizeObject()
    {
        if (RandomizationTypes[0] != null && RandomizationTypes[0].IsRandom)
        {
            Material randomMaterial = (Material)RandomizationTypes[0].GetRandomValue();
            randomMaterial.color = Material.color;
            Material = randomMaterial;
            OnUpdateSceneObjectView(this, EventArgs.Empty);
        }
    }
}
