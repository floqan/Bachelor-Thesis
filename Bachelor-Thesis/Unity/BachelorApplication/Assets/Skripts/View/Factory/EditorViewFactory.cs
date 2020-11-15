using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorViewFactory 
{
    public IEditorView View { get; private set; }

    public EditorViewFactory()
    {
        View = Camera.main.GetComponent<EditorView>();
        View.state = EditorView.State.Idle;
    }
}
