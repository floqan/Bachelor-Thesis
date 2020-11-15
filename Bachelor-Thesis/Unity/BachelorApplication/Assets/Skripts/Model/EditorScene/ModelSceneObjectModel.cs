using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSceneObjectModel: AbstractSceneObjectModel
{
    public ModelSceneObjectModel(ModelItemModel itemModel) : base(itemModel)
    {
        Parameter.Add(new PositionModel());
        Parameter.Add(new RotationModel());
        Parameter.Add(new ScaleModel());
        Parameter.Add(new ObjectColorModel());
        Parameter.Add(new TextureModel());
    }
}
