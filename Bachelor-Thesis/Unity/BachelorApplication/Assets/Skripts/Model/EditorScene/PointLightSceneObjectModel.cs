
public class PointLightSceneObjectModel : AbstractLightSceneObjectModel
{
    public PointLightSceneObjectModel(PointLightItemModel itemModel) : base(itemModel)
    {
        Parameter.Add(new PositionModel());
        Parameter.Add(new LightningColorModel());
        Parameter.Add(new LightningIntensityModel());
        Parameter.Add(new LightningRangeModel());
    }
}
