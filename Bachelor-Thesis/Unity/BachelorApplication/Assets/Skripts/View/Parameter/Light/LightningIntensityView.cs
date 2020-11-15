using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LightningIntensityView : MonoBehaviour, ISceneObjectParameterView
{
    public TMP_InputField IntensityField;

    public TextMeshProUGUI DisplayText;
    public RandomPartView RandomPartView;
    public string ParameterType { get; set; }

    public event EventHandler<StoreParameterEventArgs> OnParameterValueChanged = (sender, e) => { };
    public event EventHandler<StoreRandomizationEventArgs> OnRandomizationChanged = (sender, e) => { };

    public void Init(object value, List<RandomizationType> randomizationTypes)
    {
        ParameterType = Constants.SceneObjectParameter.LIGHTNING_INTENSITY;
        UpdateParameterView(value); 
        DisplayText.text = ParameterType;
        RandomPartView.SelectType(randomizationTypes[0]);
    }

    public void ParameterValueChanged()
    {
        StoreParameterEventArgs args = new StoreParameterEventArgs(ParameterType);
        string text = IntensityField.text.Replace(".",",");
        args.Value = float.Parse(IntensityField.text);
        OnParameterValueChanged(this, args);
    }

    public void UpdateParameterView(object value)
    {
        IntensityField.text = ((float)value).ToString(); 
    }

    public void RandomizationChanged()
    {
        OnRandomizationChanged(this, new StoreRandomizationEventArgs(RandomPartView.GetSelectedType(), 0));
    }

}
