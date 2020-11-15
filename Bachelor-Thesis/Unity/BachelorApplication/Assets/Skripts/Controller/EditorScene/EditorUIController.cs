
using System;

public class EditorUIController : IEditorUIController
{
    private IEditorUIView view;
    private EditorUIModel model;

    public EditorUIController(EditorUIModel model, IEditorUIView view)
    {
        this.model = model;
        subscribeView(view);
    }

    public void IncreaseImageCounter()
    {
        view.ImageCounter++;
    }

    public void DisplaySceneObjectParameter(object sender, SceneObjectSelectedEventArgs e)
    {
        view.DisplaySceneObjectParameter(e);
    }
    public void HideSceneObjectParameter(object sender, SceneObjectSelectedEventArgs e)
    {
        view.ClearParameterView(e);
    }

    public void SwitchUI()
    {
        view.SwitchUI();
    }

    public void subscribeView(IEditorUIView view)
    {
        this.view = view;
    }

    public void UnsubscribeView()
    {
        view = null;
    }

    public void GenerationFinished()
    {
        view.GenerationFinished();
    }
}
