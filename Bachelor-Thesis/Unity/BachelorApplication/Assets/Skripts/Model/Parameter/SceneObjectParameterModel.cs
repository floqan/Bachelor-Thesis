using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Security.Policy;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.EventSystems;

public abstract class SceneObjectParameterModel : AbstractParameterModel
{
    public List<RandomizationType> RandomizationTypes { get; set; }
    public string DisplayName { get; set; }
    public string ParameterType { get; set; }

    public abstract event EventHandler OnUpdateSceneObjectView;

    public SceneObjectParameterModel(string parameterType)
    {
        ParameterType = parameterType;
        DisplayName = parameterType;
        RandomizationTypes = new List<RandomizationType>();
    }

    public abstract GameObject GetGameObjectView();

    //Store the given value into the model and refresh
    public abstract void StoreAndApplyValueOnSceneObject(StoreParameterEventArgs args);
    
    //store the given value in the specific value of the model
    public abstract void StoreValue(StoreParameterEventArgs args);
    
    //return the specific value of the model
    public abstract object GetValue();

    //return, if the parameter should be randomized
    public bool IsRandom()
    {
        foreach (var type in RandomizationTypes)
        {
            if (type != null && type.IsRandom)
            {
                return true;
            }
        }
        return false;
    }

    //Update the correspondig value of the given scene object
    public abstract void UpdateParameterOnSceneObject(GameObject sceneObject);

    public abstract void InitFromGameObject(GameObject sceneObject);

    public abstract void RandomizeObject();
}
