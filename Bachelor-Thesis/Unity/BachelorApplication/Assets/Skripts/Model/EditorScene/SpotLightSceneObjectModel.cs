using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightSceneObjectModel : AbstractLightSceneObjectModel
{
    
    public SpotLightSceneObjectModel(SpotLightItemModel item) : base(item)
    {
        Parameter.Add(new PositionModel());
        Parameter.Add(new RotationModel());
        Parameter.Add(new LightningColorModel());
        Parameter.Add(new LightningIntensityModel());
        Parameter.Add(new LightningRangeModel());
        Parameter.Add(new LightningAngleModel());
    }

}
