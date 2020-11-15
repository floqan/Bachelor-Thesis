using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PositionView : MonoBehaviour, ISceneObjectParameterView
{

    public InputField valueX;
    public InputField valueY;
    public InputField valueZ;

    public TextMeshProUGUI DisplayText;

    public RandomPartView RandomPartView_X;
    public RandomPartView RandomPartView_Y;
    public RandomPartView RandomPartView_Z;
    public string ParameterType { get; set; }

    public event EventHandler<StoreParameterEventArgs> OnParameterValueChanged = (sender, e) => { };
    public event EventHandler<StoreRandomizationEventArgs> OnRandomizationChanged = (sender, e) => { };

    public void Init(object value, List<RandomizationType> randomizationTypes)
    {
        ParameterType = Constants.SceneObjectParameter.POSITION;
        DisplayText.text = ParameterType;
        UpdateParameterView(value);
        RandomPartView_X.SelectType(randomizationTypes[0]);
        RandomPartView_Y.SelectType(randomizationTypes[1]);
        RandomPartView_Z.SelectType(randomizationTypes[2]);
    }

    public void ParameterValueChanged()
    {
        Vector3 pos = new Vector3(valueX.text.Equals("") ? 0 : float.Parse(valueX.text), valueY.text.Equals("") ? 0 : float.Parse(valueY.text), valueZ.text.Equals("") ? 0 : float.Parse(valueZ.text));
        StoreParameterEventArgs args = new StoreParameterEventArgs(ParameterType);
        args.Value = pos;
        OnParameterValueChanged(this, args);
    }

    public void UpdateParameterView(object value)
    {
        Vector3 position = (Vector3)value;
        valueX.text = position.x.ToString();
        valueY.text = position.y.ToString();
        valueZ.text = position.z.ToString();
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
