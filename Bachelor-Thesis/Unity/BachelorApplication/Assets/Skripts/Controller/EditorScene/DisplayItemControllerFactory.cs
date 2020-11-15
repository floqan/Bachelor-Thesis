using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItemControllerFactory 
{
    public DisplayItemController<T> CreateDisplayItemController<T>(AllItemsModel<T> model) where T: AbstractItemModel
    {
        var displayItemController = new DisplayItemController<T>(model);
        return displayItemController;
    }
}
