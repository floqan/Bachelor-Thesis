using System.Collections;
using System;
using UnityEngine;


public class ArrowView : MonoBehaviour
{

    public Vector3 Direction;
    private Color StandardColor;
    public Color HighlightColor;

    public event EventHandler<MoveSelectionEventArgs> OnMoveSelection = (sender, e) => { };

    // Start is called before the first frame update
    void Start()
    {
        StandardColor = gameObject.GetComponent<Renderer>().material.color;
    }

    private void OnMouseOver()
    {
        gameObject.GetComponent<Renderer>().material.color = HighlightColor;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = StandardColor;
    }

    private void OnMouseDown()
    {
        MoveSelectionEventArgs args = new MoveSelectionEventArgs();
        args.Direction = Direction;

        OnMoveSelection(this, args);
    }
}
