using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSceneObjectModel
{ 

    public List<SceneObjectParameterModel> Parameter { get; set; }
    public AbstractItemModel ItemModel;

    public AbstractSceneObjectModel(AbstractItemModel itemModel)
    {
        ItemModel = itemModel;
        Parameter = new List<SceneObjectParameterModel>();
    }



    public bool IsRandom()
    {
        foreach(var parameter in Parameter)
        {
            if (parameter.IsRandom())
            {
                return true;
            }
        }
        return false;
    }

    public ISceneObjectView CreateView(GameObject item)
    {
        if (item.GetComponent<SceneObjectView>())
        {
            return item.GetComponent<SceneObjectView>();
        }
        else
        {
            return item.AddComponent<SceneObjectView>();
        }
    }
    
    public void RandomizeObject()
    {
        foreach(var parameter in Parameter)
        {
            parameter.RandomizeObject();
        }   
    }
}
