using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterViewFactory 
{
    public GameObject CreatePositionView()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Model/PositionView"));
    }

    public GameObject CreateRotationView()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Model/RotationView"));
    }

    public GameObject CreateScaleView()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Model/ScaleView"));
    }

    public GameObject CreateLightningIntensityView()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Light/LightningIntensityView"));
    }

    public GameObject CreateLightningColorView()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Light/LightningColorView"));
    }

    public GameObject CreateLightningRangeView()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Light/LightningRangeView"));
    }

    public GameObject CreateLightningAngleView()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Light/LightningAngleView"));
    }

    public GameObject CreateObjectColorView()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Model/ObjectColorView"));
    }

    public GameObject CreateTextureView()
    {
        return Object.Instantiate(Resources.Load<GameObject>("Model/TextureView"));
    }

    // <TO EXTEND> Create new methode for new parameterType and reference the view in the resources directory in Unity
}
