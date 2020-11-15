using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItemsModel<T> where T: AbstractItemModel
{
    private readonly List<T> Models;

    public AllItemsModel()
    {
        Models = new List<T>();
    }

    public T Get(int index)
    {
        return Models[index];
    }

    public T Get(string name)
    {
        foreach(var model in Models)
        {
            if(model.DisplayName == name)
            {
                return model;
            }
        }
        return null;
    }

    public void Add(T model)
    {
        Models.Add(model);
    }

    public bool Contains(T model)
    {
        return Models.Contains(model);
    }

    public void Remove(T model)
    {
        Models.Remove(model);
    }

    public int Length()
    {
        return Models.Count;
    }

    public bool Contains(string name)
    {
        foreach(var model in Models)
        {
            if(model.DisplayName == name)
            {
                return true;
            }
        }
        return false;
    }

    public List<T> GetModels()
    {
        return Models;
    }
}
