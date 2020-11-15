public interface IEditorUIView 
{
    int ImageCounter { get; set; }
    void SwitchTab(int index);
    void ClearParameterView(SceneObjectSelectedEventArgs e);
    void DisplaySceneObjectParameter(SceneObjectSelectedEventArgs e);
    void SwitchUI();
    void GenerationFinished();
}
