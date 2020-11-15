using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalSettingsModel : AbstractSettingsModel
{
    public GeneralParameterModel GeneralParameterModel { get; set; }
    public EditorUIModel EditorUIModel { get; set; }
    public EditorModel EditorModel { get; set; }


    public GlobalSettingsModel()
    {
        GeneralParameterModel = new GeneralParameterModel();
        EditorUIModel = new EditorUIModel();
        EditorModel = new EditorModel();
    }
}
