using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureRandomPartView : MonoBehaviour, IRandomPartView
{

    public Toggle RandomCheckbox;

    public List<RandomizationType> Types { get; set; }

    private void Awake()
    {
        InitTypes();
        RandomCheckbox.onValueChanged.AddListener(delegate { RandomChanged(); });
    }

    public RandomizationType GetSelectedType()
    {
        return Types[0];
    }

    public void InitTypes()
    {
        Types = new List<RandomizationType>();
        Types.Add(new TextureRandomizationType());
    }

    public void RandomChanged()
    {
        GetSelectedType().IsRandom = RandomCheckbox.isOn;
    }

    public bool IsChecked()
    {
        return RandomCheckbox.isOn;
    }

    public void SelectType(RandomizationType type)
    {
        if (type == null) return;

        RandomCheckbox.isOn = type.IsRandom;
    }
}
