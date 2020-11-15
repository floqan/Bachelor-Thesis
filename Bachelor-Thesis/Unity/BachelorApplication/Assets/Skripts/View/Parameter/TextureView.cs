using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextureView : MonoBehaviour, ISceneObjectParameterView
{
    public TextMeshProUGUI DisplayName;
    
    public Image Preview;
    public TextMeshProUGUI PreviewName;
    public TextureRandomPartView RandomPartView;
    public string ParameterType { get; set; }

    public event EventHandler<StoreParameterEventArgs> OnParameterValueChanged;
    public event EventHandler<StoreRandomizationEventArgs> OnRandomizationChanged = (sender, e) => { };

    public void Init(object value, List<RandomizationType> randomizationTypes)
    {
        ParameterType = Constants.SceneObjectParameter.TEXTURE;
        DisplayName.text = ParameterType;
        RandomPartView.SelectType(randomizationTypes[0]);
    }

    public void ParameterValueChanged()
    {
        StoreParameterEventArgs args = new StoreParameterEventArgs(ParameterType);
        OnParameterValueChanged(this, args);
    }

    public void UpdateParameterView(object value)
    {
        /*Material material = (Material)value;
        Texture texture = material.GetTexture("_MainTex");
        if(texture != null)
        {
            PreviewName.text = texture.name;
            Preview.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            PreviewName.text = "Color";
            Preview.color = material.color;
        }*/
    }

    public void RandomizationChanged()
    {
        OnRandomizationChanged(this, new StoreRandomizationEventArgs(RandomPartView.GetSelectedType(), 0));
    }
}
