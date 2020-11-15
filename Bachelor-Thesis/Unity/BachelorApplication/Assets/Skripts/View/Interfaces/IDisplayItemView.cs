using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

public interface IDisplayItemView 
{
    GameObject Preview { get; set; }

    event EventHandler<DisplayItemClickedArgs> OnClicked;
    void InitView(string displayName, GameObject preview, int itemIndex);
    void OnItemClicked();
    void ClearView();
}
