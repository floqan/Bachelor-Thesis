using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorUIViewFactory 
{
    public IEditorUIView View { get; private set; }

    public EditorUIViewFactory()
    {
        var instance = GameObject.FindGameObjectWithTag("Canvas");
        View = instance.GetComponent<EditorUIView>();
    }

}
