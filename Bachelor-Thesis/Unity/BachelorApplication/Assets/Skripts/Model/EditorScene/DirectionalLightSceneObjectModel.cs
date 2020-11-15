using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightSceneObjectModel : AbstractLightSceneObjectModel
{
   public DirectionalLightSceneObjectModel(DirectionalLightItemModel itemModel) : base(itemModel)
    {
        Parameter.Add(new PositionModel());
        Parameter.Add(new RotationModel());
        Parameter.Add(new LightningColorModel());
        Parameter.Add(new LightningIntensityModel());
        Parameter.Add(new LightningRangeModel());
    }
}
