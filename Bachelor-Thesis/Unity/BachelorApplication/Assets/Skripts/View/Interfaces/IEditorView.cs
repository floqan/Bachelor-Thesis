using System;
using UnityEngine;
using static EditorView;

public interface IEditorView
{
    GameObject Selection { get; set; }
    GameObject Arrow { get; set; }

    event EventHandler OnPlaceItem;
    event EventHandler<SceneObjectSelectedEventArgs> OnSceneObjectSelected;
    event Action OnSceneObjectDeselected;
    event EventHandler<SceneObjectSelectedEventArgs> OnHideParameter;
    event Action OnDefaultSunDeleted;
    State state { get; set; }
    void Destroy(GameObject sceneObject);
    void MoveArrow();
    void StartMoveSelection(object sender, MoveSelectionEventArgs e);
    void DisplayCameraPreview();
    void HideCameraPreview();
    void DeleteDefaultSun(bool defaultSunDeleted);
}
