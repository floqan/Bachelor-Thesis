
using System;
using UnityEngine;

public class SceneObjectParameterController : ISceneObjectParameterController
{
    private ISceneObjectParameterView view;
    private SceneObjectParameterModel model;


    public SceneObjectParameterController(SceneObjectParameterModel model, ISceneObjectParameterView view)
    {
        this.model = model;
        this.view = view;
        view.Init(model.GetValue(), model.RandomizationTypes);
        view.OnRandomizationChanged += RandomizationChanged;
    }

    public void RandomizationChanged(object sender, StoreRandomizationEventArgs e)
    {
        model.RandomizationTypes[e.ListIndex] = e.Type;
    }

    public void UpdateParameterView(object sender, EventArgs e)
    {
        view.UpdateParameterView(model.GetValue());
    }
}
