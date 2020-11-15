using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionSceneObjectModel : AbstractSceneObjectModel
{
    public RegionSceneObjectModel(RegionItemModel itemModel) : base(itemModel)
    {
        Parameter.Add(new PositionModel());
        Parameter.Add(new TextureModel());
        Parameter.Add(new RotationModel());
        Parameter.Add(new ScaleModel());
        Parameter.Add(new ObjectColorModel());
    }
}
