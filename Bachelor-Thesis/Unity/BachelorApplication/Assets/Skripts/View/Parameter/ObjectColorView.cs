using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectColorView : MonoBehaviour, ISceneObjectParameterView
{
    public InputField valueR;
    public InputField valueG;
    public InputField valueB;

    public Image ColorField;
    
    public TextMeshProUGUI DisplayText;

    public RandomPartView RandomPartView_R;
    public RandomPartView RandomPartView_G;
    public RandomPartView RandomPartView_B;
    public string ParameterType { get; set; }

    public event EventHandler<StoreParameterEventArgs> OnParameterValueChanged;
    public event EventHandler<StoreRandomizationEventArgs> OnRandomizationChanged = (sender, e) => { };

    public void Init(object value, List<RandomizationType> randomizationTypes)
    {
        ParameterType = Constants.SceneObjectParameter.OBJECT_COLOR;
        DisplayText.text = ParameterType;
        UpdateParameterView(value);
        RandomPartView_R.SelectType(randomizationTypes[0]);
        RandomPartView_G.SelectType(randomizationTypes[1]);
        RandomPartView_B.SelectType(randomizationTypes[2]);
    }

    public void ParameterValueChanged()
    {

        Color newColor = new Color(float.Parse(valueR.text) / 256, float.Parse(valueG.text) / 256, float.Parse(valueB.text) / 256);
        ColorField.color = newColor;
        StoreParameterEventArgs args = new StoreParameterEventArgs(ParameterType);
        args.Value = newColor;
        OnParameterValueChanged(this, args);
    }

    public void UpdateParameterView(object value)
    {
        Color color = (Color)value;
        ColorField.color = color;
        valueR.text = (color.r * 256).ToString();
        valueG.text = (color.g * 256).ToString();
        valueB.text = (color.b * 256).ToString();
    }
    public void RandomizationRChanged()
    {
        OnRandomizationChanged(this, new StoreRandomizationEventArgs(RandomPartView_R.GetSelectedType(), 0));
    }
    public void RandomizationGChanged()
    {
        OnRandomizationChanged(this, new StoreRandomizationEventArgs(RandomPartView_G.GetSelectedType(), 1));
    }
    public void RandomizationBChanged()
    {
        OnRandomizationChanged(this, new StoreRandomizationEventArgs(RandomPartView_B.GetSelectedType(), 2));
    }
}
