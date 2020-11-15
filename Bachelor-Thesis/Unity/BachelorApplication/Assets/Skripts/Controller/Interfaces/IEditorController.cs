using System;
public interface IEditorController
{
    event EventHandler<SceneObjectSelectedEventArgs> OnDisplaySceneObjectParameter;
    event EventHandler<SceneObjectSelectedEventArgs> OnHideSceneObjectParameter;
    event EventHandler<SceneObjectSelectedEventArgs> OnRequestSceneObjectParameter;
    event EventHandler OnDestroyController;
    void SubscribeView(IEditorView view);
    void Init(bool loadController);
    void UnsubscribeView();
    void LoadController();
    void DeleteSceneObjectController();
    void DeleteSelectionModel(object sender , DeleteSceneObjectEventArgs e);

}
