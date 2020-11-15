using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightningAngleView : MonoBehaviour, ISceneObjectParameterView
{
    public TextMeshProUGUI DisplayText;
    public TMP_InputField AngleInputField;
    public RandomPartView RandomPartView;
    public string ParameterType { get; set; }

    public event EventHandler<StoreParameterEventArgs> OnParameterValueChanged;
    public event EventHandler<StoreRandomizationEventArgs> OnRandomizationChanged = (sender, e) => { };

    public void Init(object value, List<RandomizationType> randomizationTypes)
    {
        ParameterType = Constants.SceneObjectParameter.LIGHTNING_SPOT_ANGLE;
        DisplayText.text = ParameterType;
        UpdateParameterView(value);
        RandomPartView.SelectType(randomizationTypes[0]);
    }

    public void ParameterValueChanged()
    {
        StoreParameterEventArgs args = new StoreParameterEventArgs(ParameterType);
        string text = AngleInputField.text.Replace(".", ",");
        args.Value = float.Parse(AngleInputField.text);
        OnParameterValueChanged(this, args);
    }

    public void UpdateParameterView(object value)
    {
        AngleInputField.text = ((float)value).ToString();
    }
    public void RandomizationChanged()
    {
        OnRandomizationChanged(this, new StoreRandomizationEventArgs(RandomPartView.GetSelectedType(), 0));
    }
}
