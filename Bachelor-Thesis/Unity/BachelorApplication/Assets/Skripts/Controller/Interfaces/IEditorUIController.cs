
using System;

public interface IEditorUIController
{
    void subscribeView(IEditorUIView view);
    void UnsubscribeView();
    void DisplaySceneObjectParameter(object sender, SceneObjectSelectedEventArgs e);
    void SwitchUI();
    void HideSceneObjectParameter(object sender, SceneObjectSelectedEventArgs e);
    void IncreaseImageCounter();
    void GenerationFinished();
}
