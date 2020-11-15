using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayItemController<T>  : IDisplayItemController where T : AbstractItemModel
{
    public List<IDisplayItemView> Views ; 
    public AllItemsModel<T>  ItemModels ;

    public event EventHandler<DisplayItemClickedArgs> CreateSceneObject = (sender, e) => { };

    public DisplayItemController(AllItemsModel<T> itemModels)
    {
        Views = new List<IDisplayItemView>();
        ItemModels = itemModels;
        
    }

    public void SubscribeView(IDisplayItemView view)
    {
        view.OnClicked += HandleClicked;
        if (!Views.Contains(view))
        {
            Views.Add(view);
        }
    }

    public void UnsubscribeView(IDisplayItemView view)
    {
        view.OnClicked -= HandleClicked;
        view.ClearView();
        Views.Remove(view);
    }

    public void HandleClicked(object sender, DisplayItemClickedArgs e)
    {
        e.ItemModel = ItemModels.Get(e.Index);
        CreateSceneObject(this, e);
    }

    public void InitView()
    {
        
    }
}
