using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class LightningRangeView : MonoBehaviour, ISceneObjectParameterView
{

    public TMP_InputField RangeField;

    public RandomPartView RandomPartView;
    public string ParameterType { get; set; }

    public TextMeshProUGUI DisplayText;
    
    public event EventHandler<StoreParameterEventArgs> OnParameterValueChanged = (sernder, e) => { };
    public event EventHandler<StoreRandomizationEventArgs> OnRandomizationChanged = (sender, e) => { };

    public void Init(object value, List<RandomizationType> randomizationTypes)
    {
        ParameterType = Constants.SceneObjectParameter.LIGHTNING_RANGE;
        DisplayText.text = ParameterType;
        UpdateParameterView(value);
        RandomPartView.SelectType(randomizationTypes[0]);

    }

    public void ParameterValueChanged()
    {
        StoreParameterEventArgs args = new StoreParameterEventArgs(ParameterType);
        string text = RangeField.text.Replace(".", ",");
        args.Value = float.Parse(RangeField.text);
        OnParameterValueChanged(this, args);
    }

    public void UpdateParameterView(object value)
    {
        RangeField.text = ((float)value).ToString();    
    }

    public void RandomizationChanged()
    {
        OnRandomizationChanged(this, new StoreRandomizationEventArgs(RandomPartView.GetSelectedType(), 0));
    }
}
