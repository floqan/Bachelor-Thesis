using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CustomScrollRect: ScrollRect
{
    public event EventHandler OnScrolled = (sender, e) => { };

    public override void OnBeginDrag(PointerEventData eventData) {}
    public override void OnDrag(PointerEventData eventData){}

    public override void OnEndDrag(PointerEventData eventData){}

    public override void OnScroll(PointerEventData data)
    {
        base.OnScroll(data);
        OnScrolled(this, EventArgs.Empty);
    }
}
