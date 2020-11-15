

using System;
using System.Collections.Generic;
using UnityEngine;

public class PositionModel : SceneObjectParameterModel
{
    private Vector3 Position;
    public override event EventHandler OnUpdateSceneObjectView = (sender, e) => { };
    public PositionModel(): base(Constants.SceneObjectParameter.POSITION)
    {
        Position = new Vector3();
        RandomizationTypes.Add(null);
        RandomizationTypes.Add(null);
        RandomizationTypes.Add(null);
    }

    public override void InitFromGameObject(GameObject sceneObject)
    {
        Position = sceneObject.transform.position;
    }

    public override GameObject GetGameObjectView()
    {
        ParameterViewFactory factory = new ParameterViewFactory();
        return factory.CreatePositionView();
    }

    public override void StoreAndApplyValueOnSceneObject(StoreParameterEventArgs args)
    {
        StoreValue(args);
        OnUpdateSceneObjectView(this, EventArgs.Empty);
    }
    public override void StoreValue(StoreParameterEventArgs args)
    {
        Position = (Vector3)args.Value;
    }

    public override object GetValue()
    {
        return Position;
    }

    public override void UpdateParameterOnSceneObject(GameObject sceneObject)
    {
        sceneObject.transform.position = Position;
        Camera.main.GetComponent<EditorView>().MoveArrow();
    }
    public override void RandomizeObject()
    {
        if (RandomizationTypes[0] != null && RandomizationTypes[0].IsRandom)
        {
            Position.x = (float) RandomizationTypes[0].GetRandomValue();
        }

        if (RandomizationTypes[1] != null && RandomizationTypes[1].IsRandom)
        {
            Position.y = (float)RandomizationTypes[1].GetRandomValue();
        }

        if (RandomizationTypes[2] != null && RandomizationTypes[2].IsRandom)
        {
            Position.z = (float)RandomizationTypes[2].GetRandomValue();
        }
        OnUpdateSceneObjectView(this, EventArgs.Empty);
    }
}
