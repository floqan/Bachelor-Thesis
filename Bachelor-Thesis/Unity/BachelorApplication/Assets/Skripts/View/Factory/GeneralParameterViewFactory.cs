using UnityEngine;

public class GeneralParameterViewFactory 
{
    public IGeneralParameterView View { get; private set;}

    public GeneralParameterViewFactory()
    {
        var instance = GameObject.FindGameObjectWithTag("Canvas");
        View = instance.GetComponent<GeneralParameterView>();
    }
}
