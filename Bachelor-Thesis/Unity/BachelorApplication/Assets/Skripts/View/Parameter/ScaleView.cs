using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScaleView : MonoBehaviour, ISceneObjectParameterView
{
    public InputField valueX;
    public InputField valueY;
    public InputField valueZ;

    public TextMeshProUGUI displayText;
    public RandomPartView RandomPartView_X;
    public RandomPartView RandomPartView_Y;
    public RandomPartView RandomPartView_Z;
    public string ParameterType { get; set; }

    public event EventHandler<StoreParameterEventArgs> OnParameterValueChanged = (sender, e) => { };
    public event EventHandler<StoreRandomizationEventArgs> OnRandomizationChanged = (sender, e) => { };

    public void Init(object value, List<RandomizationType> randomizationTypes)
    {
        ParameterType = Constants.SceneObjectParameter.SCALE;
        displayText.text = ParameterType;
        UpdateParameterView(value);
        RandomPartView_X.SelectType(randomizationTypes[0]);
        RandomPartView_Y.SelectType(randomizationTypes[1]);
        RandomPartView_Z.SelectType(randomizationTypes[2]);
    }

    public void ParameterValueChanged()
    {
        Vector3 scale = new Vector3(float.Parse(valueX.text), float.Parse(valueY.text), float.Parse(valueZ.text));
        StoreParameterEventArgs args = new StoreParameterEventArgs(ParameterType);
        args.Value = scale;
        OnParameterValueChanged(this, args);
    }

    public void UpdateParameterView(object value)
    {
        Vector3 scale = (Vector3)value;
        valueX.text = scale.x.ToString();
        valueY.text = scale.y.ToString();
        valueZ.text = scale.z.ToString();
    }

    public void RandomizationXChanged()
    {
        OnRandomizationChanged(this, new StoreRandomizationEventArgs(RandomPartView_X.GetSelectedType(), 0));
    }
    public void RandomizationYChanged()
    {
        OnRandomizationChanged(this, new StoreRandomizationEventArgs(RandomPartView_Y.GetSelectedType(), 1));
    }
    public void RandomizationZChanged()
    {
        OnRandomizationChanged(this, new StoreRandomizationEventArgs(RandomPartView_Z.GetSelectedType(), 2));
    }
}
