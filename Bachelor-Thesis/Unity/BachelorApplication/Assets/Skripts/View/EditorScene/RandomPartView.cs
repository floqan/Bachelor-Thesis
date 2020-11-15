using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class RandomPartView : MonoBehaviour, IRandomPartView
{

    public Toggle RandomCheckbox;
    public TMP_Dropdown TypeSelection;
    public TMP_InputField FirstValue;
    public TMP_InputField SecondValue;
    public Transform ParameterPanel;
    public TextMeshProUGUI LabelField;
    public List<RandomizationType> Types { get; set; }

    private void Awake()
    {
        InitTypes();
        ToggleParameterPanel(RandomCheckbox);
        InitSelection();
        ChangeLabel();
        RandomCheckbox.onValueChanged.AddListener( delegate { ToggleParameterPanel(RandomCheckbox); });
        TypeSelection.onValueChanged.AddListener(delegate { SelectionChanged(TypeSelection);  });
        FirstValue.onEndEdit.AddListener(delegate { ValuesChanged(FirstValue);  });
        SecondValue.onEndEdit.AddListener(delegate { ValuesChanged(SecondValue);  });
    }

    public RandomizationType GetSelectedType()
    {
        return Types[TypeSelection.value];
    }

    public bool IsChecked()
    {
        return RandomCheckbox.isOn && !string.IsNullOrEmpty(FirstValue.text) && !string.IsNullOrEmpty(SecondValue.text);
    }

    public void ToggleParameterPanel(Toggle toggle)
    {
        ParameterPanel.gameObject.SetActive(RandomCheckbox.isOn);
        GetSelectedType().IsRandom = RandomCheckbox.isOn;
    }

    public void ChangeLabel()
    {
        LabelField.text = GetSelectedType().Label;
    }

    public void SelectType(RandomizationType type) 
    {
        if (type == null) return;
        
        for (int i = 0; i < Types.Count; i++)       
        {
            if(type.GetType() == Types[i].GetType())
            {
                TypeSelection.SetValueWithoutNotify(i);
                Types[i] = type;

                RandomCheckbox.isOn = type.IsRandom;
                SelectionChanged(TypeSelection);
                return;
            }
        }
    }

    public void SelectionChanged(TMP_Dropdown dropDown)
    {
        ChangeLabel();
        foreach(var type in Types)
        {
            type.IsRandom = false;
        }
        var Type = GetSelectedType();
        Type.IsRandom = RandomCheckbox.isOn;
        FirstValue.text = "" + (float?)Type.GetValues()[0];
        SecondValue.text = "" + (float?)Type.GetValues()[1];
        ToggleParameterPanel(RandomCheckbox);
    }

    public void ValuesChanged(TMP_InputField _field)
    {
        
        List<object> values = new List<object>();
        float one, two;
        if (FirstValue.text.Equals(""))
        {
            values.Add(null);
        }
        else
        {
            float.TryParse(FirstValue.text, out one);
            values.Add(one);
        }
        if (SecondValue.text.Equals(""))
        {
            values.Add(null);
        }
        else
        {
            float.TryParse(SecondValue.text, out two);
            values.Add(two);
        }
        GetSelectedType().SetValues(values);
    }

    public void InitTypes()
    {
        Types = new List<RandomizationType>();
        Types.Add(new UniformDistributionRandomizationType());
        Types.Add(new NormalDistributionRandomizationType());

        //<TO EXTEND> Add new Randomization Type
    }
    private void InitSelection()
    {
        List<TMP_Dropdown.OptionData> dataList = new List<TMP_Dropdown.OptionData>();
        foreach(var type in Types)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            data.text = type.Name;
            data.image = type.Sprite;
            dataList.Add(data);
        }
        TypeSelection.AddOptions(dataList);
    }
}
